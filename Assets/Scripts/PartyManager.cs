using System.Collections;
using UnityEngine;

public class PartyManager : MonoBehaviour {

    //All of the objects specific to the party scene
    public GameObject[] partyObjs;

    //Start the scenario from the beginning
    public void Run()
    {
        

        /*
        for (int i = 0; i < partyObjs.Length; i++)
            partyObjs[i].SetActive(true);
        */
    }

    //End the scenario
    public void Stop()
    {
        for (int i = 0; i < partyObjs.Length; i++)
            partyObjs[i].SetActive(false);
    }
}
