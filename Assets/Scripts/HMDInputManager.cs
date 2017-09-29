using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMDInputManager : MonoBehaviour {

    public PhotographManager photoManager;

    //When space is pressed, the material will appear, with a
    //snapshot from the webcam.
    void Update()
    {
        //In the future this will be replaced with gaze detection and a timer
        if (Input.GetKeyDown(KeyCode.Space))
        {
            photoManager.TakePicture(this.transform);
        }
    }
}
