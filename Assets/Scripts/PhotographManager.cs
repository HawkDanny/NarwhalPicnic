using UnityEngine;
using UnityEngine.UI;

public class PhotographManager : MonoBehaviour {

    //Prefab of the snapshots being taken (Currently has to be a quad)
    public GameObject snapshotPrefab;
    //Length it takes to take a picture
    public int prepLength;
    //Length between the moment a picture is taken, and recharging
    public int standbyLength;
    //The circle UI
    public Image circle;
    //The transform of where the snapshot is spawned
    public Transform SpawnTransform;


    //WebCamTexture that the vive camera renders to.
    //Treated as a live video, but we just lift frames from it
    private WebCamTexture webcamTexture;
    //Enum used to track the camera's state
    private enum CameraState { Off, TakingPicture, Rewinding, Recharging, Standby }
    //The current state of this current camera
    private CameraState currentState;
    //Current progress of prep time
    private float prepProgress;
    //Current progress of standby time
    private float standbyProgress;


    void Start()
    {
        //Create the webcamTexture and start it
        webcamTexture = new WebCamTexture();
        webcamTexture.Play();

        //Set up camera values and ui
        currentState = CameraState.Off;
        prepProgress = (float)prepLength;
        standbyProgress = 0f;
    }

    void Update()
    {
        #region Camera Switch
        //Controls the updating of the ui progress, based on state
        switch (currentState)
        {
            //Off
            case CameraState.Off:
                circle.enabled = false;
                break;
            //When the bar is being depleted, and a picture is being prepped to be taken
            case CameraState.TakingPicture:
                circle.enabled = true;

                if ((--prepProgress) <= 0)
                {
                    TakePicture(SpawnTransform);
                    currentState = CameraState.Standby;
                }
                break;
            //If the gaze shifts before a picture is taken, immediately start rewinding. This can be interrupted again though
            case CameraState.Rewinding:
                if ((++prepProgress) >= prepLength)
                    currentState = CameraState.Off;
                break;
            //When the picture has been taken, and the bar needs to recharge before taking a new picture
            //This is the same code as Rewinding, but they are different states because they're handled separately when called
            case CameraState.Recharging:
                if ((++prepProgress) >= prepLength)
                    currentState = CameraState.Off;
                break;
            case CameraState.Standby:
                if ((++standbyProgress) >= standbyLength)
                {
                    currentState = CameraState.Recharging;
                    standbyProgress = 0;
                }
                break;
        }
        #endregion

        //Apply the length of the ui progress
        circle.fillAmount = prepProgress / prepLength;
    }

    //Try and start to take a picture. If the camera is recharging, return
    public void StartShot()
    {
        if (currentState == CameraState.Recharging || currentState == CameraState.Standby)
            return;
        currentState = CameraState.TakingPicture;
    }

    //Stop taking a picture. Lower the camera and rewind the progress (If already recharging, return)
    public void AbandonShot()
    {
        if (currentState == CameraState.Recharging || currentState == CameraState.Standby)
            return;
        currentState = CameraState.Rewinding;
    }

    //Spawn new prefab and copy the webcamTexture to it. Drop it into the world
    public void TakePicture(Transform spawnLoc)
    {
        GameObject snapshot = Instantiate(snapshotPrefab, spawnLoc.transform.position, spawnLoc.transform.rotation);
        //Create new renderTexture
        RenderTexture rendTex = new RenderTexture(256, 256, 24);
        snapshot.GetComponent<Renderer>().material.SetTexture("_MainTex", rendTex);
        Graphics.Blit(webcamTexture, rendTex);
    }
}
