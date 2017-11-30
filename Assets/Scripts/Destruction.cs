using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour {

    //The mass of each building piece
    public float pieceMass;

    //If hit by a wrecking ball, add a rigidbody to each building piece and then unparent them
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wrecking"))
            print("collided with a wrecking ball");

        for (int i = 0; i < this.transform.childCount; i++)
        {
            Rigidbody rb = this.transform.GetChild(i).gameObject.AddComponent<Rigidbody>();
            rb.mass = pieceMass;
        }

        this.transform.DetachChildren();
    }
}
