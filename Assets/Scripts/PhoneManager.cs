using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour {

    //The data used to create the monitor instructions
    public Instructions instructions;

    //All of the objects specific to the construction scene
    public GameObject[] phoneObjs;

    //Whether or not the phone has been answered
    [HideInInspector]
    public bool answeredPhone = false;

    /// <summary>
    /// Creates a fresh new scenario by activating a random phone hat and 
    /// </summary>
    public void Run()
    {
        bool isAnActivePhone = false;
        //bool activateNextPhone = false;

        //Activate all the in-world prefabs
        //There is a 50% chance the first phone will be activated, and if it isn't then the other phone is
        for (int i = 0; i < phoneObjs.Length; i++)
        {
            if (phoneObjs[i].CompareTag("Phone"))
            {
                /*
                if (activateNextPhone)
                {
                    phoneObjs[i].SetActive(true);
                    continue;
                }

                if (Random.value < 0.5f)
                {
                    phoneObjs[i].SetActive(true);
                }
                else
                {
                    activateNextPhone = true;
                }*/

                if (isAnActivePhone)
                    continue;

                phoneObjs[i].SetActive(true);
                isAnActivePhone = true;
            }
            else
                phoneObjs[i].SetActive(true);
        }
    }

    //End the scenario
    public void Stop()
    {
        for (int i = 0; i < phoneObjs.Length; i++)
            phoneObjs[i].SetActive(false);
    }
}
