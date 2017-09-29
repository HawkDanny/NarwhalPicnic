using UnityEngine;
using UnityEngine.UI;

//DEPRECATED
public class PhotographTimer : MonoBehaviour {

    public int prepLength;
    public int standybyLength;
    public Image circle;

    private enum CameraState { Off, TakingPicture, Rewinding, Recharging, Standby}
    private CameraState currentState = CameraState.Off;
    private float prepProgress;


	void Start () {
        prepProgress = (float)prepLength;

        circle = this.GetComponent<Image>();    
	}
	
	// Update is called once per frame
	void Update () {

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

                if ((--prepProgress * Time.deltaTime) <= 0)
                {
                    TakePicture();
                    currentState = CameraState.Recharging;
                }
                break;
            //If the gaze shifts before a picture is taken, immediately start rewinding. This can be interrupted again though
            case CameraState.Rewinding:
                if ((++prepProgress * Time.deltaTime) >= prepLength)
                    currentState = CameraState.Off;
                break;
            //When the picture has been taken, and the bar needs to recharge before taking a new picture
            //This is the same code as Rewinding, but they are different states because they're handled separately when called
            case CameraState.Recharging:
                if ((++prepProgress * Time.deltaTime) >= prepLength)
                    currentState = CameraState.Off;
                break;
            case CameraState.Standby:

                break;
        }

        //Apply the length of the ui progress
        circle.fillAmount = prepProgress / prepLength;
	}

    //Try and start to take a picture. If the camera is recharging, return
    public void StartShot ()
    {
        if (currentState == CameraState.Recharging)
            return;
        currentState = CameraState.TakingPicture;
    }

    //Stop taking a picture. Lower the camera and rewind the progress (If already recharging, return)
    public void AbandonShot()
    {
        if (currentState == CameraState.Recharging)
            return;
        currentState = CameraState.Rewinding;
    }

    //After the camera is fully raised, take a picture
    public void TakePicture()
    {
        //TODO: Handle animations and what happens when a picture is taken
    }
}
