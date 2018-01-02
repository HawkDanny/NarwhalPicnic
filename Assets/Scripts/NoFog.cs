using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoFog : MonoBehaviour {

    /*
     * This script will disable fog for any camera it is attached to.
     * 
     * It does so by changing the global render settings for fog
     * immediately before and after the camera renders.
     * 
     * I do not know how optimized it is
     */

    private bool isThereFogInThisScene;

    private void Start()
    {
        isThereFogInThisScene = RenderSettings.fog;
    }

    private void OnPreRender()
    {
        RenderSettings.fog = false;
    }

    private void OnPostRender()
    {
        RenderSettings.fog = isThereFogInThisScene;
    }
}
