using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRManager : MonoBehaviour {

    private Valve.VR.HmdQuad_t rect;
    private Vector3[] corners = new Vector3[4];
    private float minX = float.MaxValue;
    private float maxX = float.MinValue;
    private float minZ = float.MaxValue;
    private float maxZ = float.MinValue;

    #region Properties
    /// <summary>
    /// An array containing the vertex information for the base 4 corners of the play area
    /// </summary>
    public Vector3[] Corners { get { return corners; } }

    /// <summary>
    /// The minimum x value for a corner of the play area bounds
    /// </summary>
    public float MinX { get { return minX; } }

    /// <summary>
    /// The maximum x value for a corner of the play area bounds
    /// </summary>
    public float MaxX { get { return maxX; } }

    /// <summary>
    /// The minimum z value for a corner of the play area bounds
    /// </summary>
    public float MinZ { get { return minZ; } }

    /// <summary>
    /// The maximum z value for a corner of the play area bounds
    /// </summary>
    public float MaxZ { get { return maxZ; } }
    #endregion

    public void Start()
    {
        //Started in a coroutine because steamVR might not be ready, so it'll try until it is
        StartCoroutine("RetrieveBounds");
    }

    //Get the four bottom corners of the play area

    /// <summary>
    /// Coroutine that converts the corners of the play area into vector3s
    /// and stores them in the 'corners' array
    /// </summary>
    IEnumerator RetrieveBounds()
    {
        while (!SteamVR_PlayArea.GetBounds(SteamVR_PlayArea.Size.Calibrated, ref rect))
            yield return new WaitForSeconds(0.1f);

        corners[0] = new Vector3(rect.vCorners0.v0, rect.vCorners0.v1, rect.vCorners0.v2);
        corners[1] = new Vector3(rect.vCorners1.v0, rect.vCorners1.v1, rect.vCorners1.v2);
        corners[2] = new Vector3(rect.vCorners2.v0, rect.vCorners2.v1, rect.vCorners2.v2);
        corners[3] = new Vector3(rect.vCorners3.v0, rect.vCorners3.v1, rect.vCorners3.v2);

        //Set the fields for AABB collision checking with the bounds
        for (int i = 0; i < corners.Length; i++)
        {
            minX = Mathf.Min(minX, corners[i].x);
            maxX = Mathf.Max(maxX, corners[i].x);
            minZ = Mathf.Min(minZ, corners[i].z);
            maxZ = Mathf.Max(maxZ, corners[i].z);
        }
    }
}
