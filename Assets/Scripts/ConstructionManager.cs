using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionManager : MonoBehaviour {

    //The data used to create the monitor instructions
    public Instructions instructions;

    //The junk list contains everything that will be destroyed on scenario change
    private List<GameObject> junk = new List<GameObject>();

    //All of the objects specific to the construction scene
    public GameObject[] constructionObjs;

    //The tips of the narwhals that will be used to hang the wreckingballs
    public GameObject[] tips;

    public GameObject wreckingBallPrefab;
    public GameObject buildingPrefab;
    public bool isDemolished = false;

    /// <summary>
    /// Creates a fresh new scenario by spawning a new building and activating the construction hats
    /// </summary>
    public void Run()
    {
        //Activate all the in-world prefabs
        for (int i = 0; i < constructionObjs.Length; i++)
            constructionObjs[i].SetActive(true);

        //Instantiate the wrecking balls and child them to the narwhals
        for (int i = 0; i < tips.Length; i++)
        {
            GameObject ball = GameObject.Instantiate(wreckingBallPrefab,
                                   new Vector3(tips[i].transform.position.x,
                                               tips[i].transform.position.y - 0.21f,
                                               tips[i].transform.position.z),
                                   Quaternion.identity);

            ball.transform.GetChild(ball.transform.childCount - 1).GetComponent<ConfigurableJoint>().connectedBody = tips[i].GetComponent<Rigidbody>();
            AddToJunk(ball);
        }

        //Instantiate the building
        GameObject building = GameObject.Instantiate(buildingPrefab, new Vector3(0.634f, 0.638f, -0.573f), Quaternion.identity);
        building.GetComponent<Destruction>().constructionMan = this;
        AddToJunk(building);
    }

    //End the scenario
    public void Stop()
    {
        for (int i = 0; i < constructionObjs.Length; i++)
            constructionObjs[i].SetActive(false);

        //Destroy the junk and clear the list
        for (int i = 0; i < junk.Count; i++)
        {
            GameObject.Destroy(junk[i]);
            junk.RemoveAt(i);
            --i;
        }
    }

    /// <summary>
    /// Add something to the junk list, which is destroyed on scenario change
    /// </summary>
    /// <param name="obj">Gameobject to be added to the junk list</param>
    public void AddToJunk(GameObject obj)
    {
        junk.Add(obj);
    }
}
