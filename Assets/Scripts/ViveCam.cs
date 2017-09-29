using UnityEngine;

public class ViveCam : MonoBehaviour {

    private bool webcamRunning;
    private WebCamTexture webcamTexture;
    private Renderer materialRenderer;


    void Start () {
        webcamRunning = true;

        webcamTexture = new WebCamTexture();
        materialRenderer = GetComponent<Renderer>();
        materialRenderer.material.mainTexture = webcamTexture;
        webcamTexture.Play();

        materialRenderer.enabled = false;
    }

    //When space is pressed, the material will appear, with a
    //snapshot from the webcam.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (webcamRunning)
            {
                materialRenderer.enabled = true;
                webcamTexture.Pause();
            }
            else
            {
                materialRenderer.enabled = false;
                webcamTexture.Play();
            }

            webcamRunning = !webcamRunning;
        }
    }
}
