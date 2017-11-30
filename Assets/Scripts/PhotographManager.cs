using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PhotographManager : MonoBehaviour {

    //Prefab of the snapshots being taken (Currently has to be a quad)
    public GameObject photographPrefab;
    //The transform of where the snapshot is spawned
    public Transform SpawnTransform;
    //The horizontal torque added to the photograph
    public float spinAmount;
    //The light object that flashes when a picture is taken
    public Light flash;
    //The length that the flash exists
    public float flashLength;


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
        StartCoroutine("Flash");

        GameObject photo = Instantiate(photographPrefab, spawnLoc.transform.position, spawnLoc.transform.rotation);
        photo.GetComponent<Rigidbody>().AddRelativeTorque(transform.forward * Random.Range(-spinAmount, spinAmount), ForceMode.Impulse);

        //Create new renderTexture
        RenderTexture rendTex = new RenderTexture(256, 256, 24);
        photo.transform.GetChild(0).GetComponent<Renderer>().material.SetTexture("_MainTex", rendTex);
        Graphics.Blit(webcamTexture, rendTex);
    }

    //MY FIRST COROUTINE LET'S GOOOOOOOOoooooooo.......
    IEnumerator Flash()
    {
        flash.intensity = 3;
        yield return new WaitForSeconds(flashLength);
        flash.intensity = 0;
    }
}