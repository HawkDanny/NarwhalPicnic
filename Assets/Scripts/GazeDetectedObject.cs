using UnityEngine;
using UnityEngine.UI;

public class GazeDetectedObject : MonoBehaviour
{
    public PhotographManager photoManager;

    // Update is called once per frame
    void Update()
    {

        // Get this object's position
        Vector3 pos3 = this.transform.position;
        Vector4 pos4 = new Vector4(pos3.x, pos3.y, pos3.z, 1);

        // Grab camera matrices
        Matrix4x4 view = Camera.main.worldToCameraMatrix;
        Matrix4x4 proj = Camera.main.projectionMatrix;
        Matrix4x4 viewProj = proj * view;

        // Get the position in screen space
        Vector4 screenSpacePos = viewProj * pos4;
        screenSpacePos.x /= screenSpacePos.w;
        screenSpacePos.y /= screenSpacePos.w;
        screenSpacePos.z /= screenSpacePos.w;

        //Call either StartShot or AbandonShot every frame
        if (IsInCenterView(screenSpacePos))
            photoManager.StartShot();
        else
            photoManager.AbandonShot();   
    }

    private bool IsInCenterView(Vector3 screenSpacePos)
    {
        float a = screenSpacePos.x * screenSpacePos.x;
        float b = screenSpacePos.y * screenSpacePos.y;

        //If the object is in the middle half of view, return true
        return (Mathf.Sqrt(a + b) < 0.25);
        
    }
}
