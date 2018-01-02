using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour {

    public ConstructionManager constructionMan;

    //The mass of each building piece
    public float pieceMass;

    //If hit by a wrecking ball, add a rigidbody to each building piece and then unparent them
    void OnTriggerEnter(Collider other)
    {
        //For each child, add a rigidbody, add it to the junk array, and detach it
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform child = this.transform.GetChild(i);

            Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
            rb.mass = pieceMass;

            constructionMan.AddToJunk(child.gameObject);
        }

        constructionMan.isDemolished = true;

        this.transform.DetachChildren();
    }
}
