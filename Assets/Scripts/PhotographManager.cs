using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PhotographManager : MonoBehaviour {

    //The canvas taht displayes to the monitor camera
    public Canvas canvas;
    //Prefab of the snapshots being taken (Currently has to be a quad)
    public GameObject photographPrefab;
    //Prefab of the snapshot replicated to the monitor
    public GameObject monitorPhotographPrefab;
    //The transform of where the snapshot is spawned
    public Transform SpawnTransform;
    //The transform where the duplicate photo is spawned
    public Transform MonitorSpawnTransform;
    //The horizontal torque added to the photograph
    public float spinAmount;
    //The light object that flashes when a picture is taken
    public Light flash;
    //The length that the flash exists
    public float flashLength;

    public GameManager gameMan;
    public GameObject picnicTrigger;

    private GameObject currentMonitorPhoto = null;


    //WebCamTexture that the vive camera renders to.
    //Treated as a live video, but we just lift frames from it
    private WebCamTexture webcamTexture;


    void Start()
    {
        //Create the webcamTexture and start it
        webcamTexture = new WebCamTexture();
        webcamTexture.Play();
    }

    //Spawn new prefab and copy the webcamTexture to it. Drop it into the world
    public void TakePicture(Transform spawnLoc)
    {
        //Flash the camera
        //StartCoroutine("Flash");


        GameObject photo = Instantiate(photographPrefab, spawnLoc.transform.position, spawnLoc.transform.rotation);
        photo.GetComponent<Rigidbody>().AddRelativeTorque(transform.forward * Random.Range(-spinAmount, spinAmount), ForceMode.Impulse);

        //The photo that appears on the monitor
        GameObject monitorPhoto = Instantiate(monitorPhotographPrefab, MonitorSpawnTransform.position, MonitorSpawnTransform.rotation);
        monitorPhoto.transform.SetParent(canvas.transform);
        //Destroy the old photo
        GameObject.Destroy(currentMonitorPhoto);
        currentMonitorPhoto = monitorPhoto;

        //Create new renderTexture
        RenderTexture rendTex = new RenderTexture(256, 256, 24);
        photo.transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_MainTex", rendTex);
        monitorPhoto.transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_MainTex", rendTex);
        Graphics.Blit(webcamTexture, rendTex);

        if (picnicTrigger.GetComponent<ContainsNarwhals>().DoesItContainAllNarwhals())
        {
            //gameMan.RunNextScenario();
        }
    }

    //MY FIRST COROUTINE LET'S GOOOOOOOOoooooooo.......
    IEnumerator Flash()
    {
        flash.intensity = 3;
        yield return new WaitForSeconds(flashLength);
        flash.intensity = 0;


    }
}