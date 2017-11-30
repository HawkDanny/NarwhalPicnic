using UnityEngine;
using UnityEngine.UI;

/**
 *  Controls the fading of all elements into existence. The player should start
 *  in the environment with nothing to interact with. They are provided
 *  Instructions, and corresponding with the instructions, the objects that 
 *  each instruction pertains to will pop into view.
*/
public class TutorialManager : MonoBehaviour {

    public bool skipIntro;

    //Reference to the gamemanager
    public GameManager gm;

    //Timings
    public float startupTextTime;

    //There has to be a better way to do this
    public GameObject narwhal1;
    public GameObject narwhal2;
    public GameObject physicalCamera;
    public Text startupText;
    public Text narwhalText;
    public Text cameraText;

    private enum IntroState
    {
        Startup,
        StartupBuffer,
        NarwhalInstruction,
        NarwhalAppearance,
        CameraInstruction,
        CameraAppearing
    }

    private IntroState currentIntroState;
    private float timeStamp;

	// Use this for initialization
	void Start () {
        currentIntroState = IntroState.Startup;

        startupText.canvasRenderer.SetAlpha(0f);
        narwhalText.canvasRenderer.SetAlpha(0f);
        cameraText.canvasRenderer.SetAlpha(0f);

        if (skipIntro)
        {
            //Progress to the next state
            
            return;
        }

        //Start with all of these assets off
        narwhal1.SetActive(false);
        narwhal2.SetActive(false);
        physicalCamera.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (skipIntro)
            return;

        #region IntroStateMachine
        switch (currentIntroState) {
            case IntroState.Startup:
                //Show some GUI saying something about the game
                startupText.CrossFadeAlpha(1f, startupTextTime, true);
                timeStamp = Time.realtimeSinceStartup;
                currentIntroState = IntroState.StartupBuffer;
                break;
            case IntroState.StartupBuffer:
                //Check if the text has faded out
                if (Time.realtimeSinceStartup - timeStamp > startupTextTime + startupTextTime) {
                    currentIntroState = IntroState.NarwhalInstruction;
                }
                //Check if the text has faded in
                else if (Time.realtimeSinceStartup - timeStamp > startupTextTime)
                {
                    startupText.CrossFadeAlpha(0f, startupTextTime, true);
                }
                break;
            case IntroState.NarwhalInstruction:
                //Introduce the narwhals
                narwhalText.CrossFadeAlpha(1f, startupTextTime, false);
                break;
            case IntroState.NarwhalAppearance:
                //Activate the narwhals
                narwhal1.SetActive(true);
                narwhal2.SetActive(true);
                currentIntroState = IntroState.CameraInstruction;
                break;
            case IntroState.CameraInstruction:
                //Introduce the camera
                break;
            case IntroState.CameraAppearing:
                //Activate the camera
                physicalCamera.SetActive(true);
                break;
        }
        #endregion
    }
}
