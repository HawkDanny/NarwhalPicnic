using UnityEngine;

public class KelpCreator : MonoBehaviour {

    //The height of the kelp in units of kelp
    public int height;

    //The kelp object
    public GameObject kelp;

    private float kelpPieceYOffset = 0.787f;


    void Start () {

        this.transform.Rotate(new Vector3(0f, Random.Range(0, 360), 0f));
        int heightVariation = (int)Random.Range(0, height / 5f); 

        //Loop and spawn kelp pieces as high as the height goes
        for (int i = 1; i <= height - heightVariation; i++)
        {
            GameObject dummyKelp = Instantiate(kelp, new Vector3(this.transform.position.x, this.transform.position.y + kelpPieceYOffset * i, this.transform.position.z), this.transform.rotation);
            dummyKelp.transform.parent = this.transform;
        }
	}
}
