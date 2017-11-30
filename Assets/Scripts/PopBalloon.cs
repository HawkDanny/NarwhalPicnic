using UnityEngine;

public class PopBalloon : MonoBehaviour {

    public ParticleSystem confetti;
    public ushort rumbleLength;

    void OnTriggerEnter(Collider other)
    {
        confetti.Play();

        int index = (int)other.gameObject.GetComponentInParent<SteamVR_TrackedController>().controllerIndex;

        print(index);

        //Vibrate the controller
        SteamVR_Controller.Input(index).TriggerHapticPulse(rumbleLength);

        this.gameObject.SetActive(false);
    }
	
}
