using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour {

    //The data used to create the monitor instructions
    public Instructions instructions;

    //The junk list contains everything that will be destroyed on scenario change
    private List<GameObject> junk = new List<GameObject>();

    //All of the objects specific to the party scene
    public GameObject[] partyObjs;

    public GameObject balloonPrefab;

    //The number of balloons spawned
    public int numBalloons;

    [HideInInspector]
    public int poppedBalloons;

    /// <summary>
    /// Creates a fresh new scenario by spawning new balloons and activating the party hats
    /// </summary>
    /// <param name="vrMan">The VRManager found in the GameManager</param>
    public void Run(VRManager vrMan)
    {
        //Activate all of the in-world prefabs
        for (int i = 0; i < partyObjs.Length; i++)
            partyObjs[i].SetActive(true);

        //Add the balloons
        for (int i = 0; i < numBalloons; i++)
        {
            GameObject balloon = GameObject.Instantiate(balloonPrefab,
                                                        new Vector3(Random.Range(vrMan.MinX, vrMan.MaxX),
                                                            Random.Range(0.6f, 1.3f),
                                                            Random.Range(vrMan.MinZ, vrMan.MaxZ)),
                                                        Quaternion.identity);
            balloon.transform.GetChild(0).GetChild(0).GetComponent<PopBalloon>().partyMan = this;
            junk.Add(balloon);

            //TODO: Random balloon color
            //TODO: Make it so balloons do not spawn in each other
        }
    }

    /// <summary>
    /// Called to clean up the scenario. Deactivates the hats and destroys the balloon gameobjects
    /// </summary>
    public void Stop()
    {
        //Deactivate all of the in-world prefabs
        for (int i = 0; i < partyObjs.Length; i++)
            partyObjs[i].SetActive(false);

        //Destroy the junk and clear the list
        for (int i = 0; i < junk.Count; i++)
        {
            GameObject.Destroy(junk[i]);
            junk.RemoveAt(i);
            --i;
        }
    }
}
