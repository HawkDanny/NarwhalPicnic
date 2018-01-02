using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainsNarwhals : MonoBehaviour {

    private int narwhalCount = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Narwhal"))
            ++narwhalCount;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Narwhal"))
            --narwhalCount;
    }

    public bool DoesItContainAllNarwhals()
    {
        if (narwhalCount >= 2)
        {
            print("contains narwhals");
            return true;
        }
        else
            return false;
    }
}
