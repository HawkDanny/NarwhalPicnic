using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerThePhone : MonoBehaviour {

    //A reference to the scene's phone manager
    public PhoneManager phoneMan;

    //The number of seconds it takes to answer the phone
    public float phoneAnswerTime;
    private float currentTime = 0f;

    private bool isPhoneBeingAnswered = false;

    /// <summary>
    /// If the phone has been answered for a set amount of time, reflect that in the manager
    /// </summary>
	void Update ()
    {
        if (isPhoneBeingAnswered)
        {
            currentTime += Time.deltaTime;
            if (currentTime > phoneAnswerTime)
            {
                phoneMan.answeredPhone = true;
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Phone"))
        {
            isPhoneBeingAnswered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Phone"))
        {
            isPhoneBeingAnswered = false;
            currentTime = 0f;
        }
    }
}
