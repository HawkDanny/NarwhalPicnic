using UnityEngine;

public class ConstructionManager : MonoBehaviour {

    //All of the objects specific to the party scene
    public GameObject[] constructionObjs;

    //Start the scenario from the beginning
    public void Run()
    {
        for (int i = 0; i < constructionObjs.Length; i++)
        {
            constructionObjs[i].SetActive(true);
            if (constructionObjs[i].CompareTag("wrecking"))
            {
                constructionObjs[i].transform.parent = null; //Don't know if this line of code works
            }
        }
    }

    //End the scenario
    public void Stop()
    {
        for (int i = 0; i < constructionObjs.Length; i++)
            constructionObjs[i].SetActive(false);
    }
}
