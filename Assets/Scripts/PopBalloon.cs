using UnityEngine;

public class PopBalloon : MonoBehaviour {

    public PartyManager partyMan;

    public ParticleSystem confetti;
    public ushort rumbleLength;

    void OnTriggerEnter(Collider other)
    {
        //Balloons cannot trigger eachother!
        if (!other.gameObject.CompareTag("Tip"))
            return;

        confetti.Play();

        //Vibrate the controller
        int index = (int)other.gameObject.GetComponentInParent<SteamVR_TrackedController>().controllerIndex;
        SteamVR_Controller.Input(index).TriggerHapticPulse(rumbleLength);

        partyMan.poppedBalloons += 1;

        this.transform.parent.gameObject.SetActive(false);
    }
	
}
