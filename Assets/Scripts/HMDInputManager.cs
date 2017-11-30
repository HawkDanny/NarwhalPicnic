using UnityEngine;

public class HMDInputManager : MonoBehaviour {

    public PhotographManager photoManager;

    public Transform SpawnTransform;

    //When space is pressed, the material will appear, with a
    //snapshot from the webcam.
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            photoManager.TakePicture(SpawnTransform);
        }
    }
}
