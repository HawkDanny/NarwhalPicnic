using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRManager : MonoBehaviour {

    private Valve.VR.HmdQuad_t rect;
    private Vector3 corner0;
    private Vector3 corner1;
    private Vector3 corner2;
    private Vector3 corner3;

    public Vector3 Corner0 { get { return corner0; } }
    public Vector3 Corner1 { get { return corner1; } }
    public Vector3 Corner2 { get { return corner2; } }
    public Vector3 Corner3 { get { return corner3; } }

    public void Start()
    {
        //Started in a coroutine because steamVR might not be ready, so it'll try until it is
        StartCoroutine("RetrieveBounds");
    }

    //Get the four bottom corners of the play area
    IEnumerator RetrieveBounds()
    {
        while (!SteamVR_PlayArea.GetBounds(SteamVR_PlayArea.Size.Calibrated, ref rect))
            yield return new WaitForSeconds(0.1f);

        //playArea = cameraRig.GetComponent<SteamVR_PlayArea>();

        corner0.Set(rect.vCorners0.v0, rect.vCorners0.v1, rect.vCorners0.v2);
        corner1.Set(rect.vCorners1.v0, rect.vCorners1.v1, rect.vCorners1.v2);
        corner2.Set(rect.vCorners2.v0, rect.vCorners2.v1, rect.vCorners2.v2);
        corner3.Set(rect.vCorners3.v0, rect.vCorners3.v1, rect.vCorners3.v2);
    }
}
