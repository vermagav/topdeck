using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class ComputerPanel : MonoBehaviour {

	private bool powerState = false;
	private bool isActive = false;

	public AnimateDoor animateDoor;
	public ComputerScreen computerScreen;

	public AudioClip voiceOver_1;
	public AudioClip voiceOver_2;

	public void PowerOn() {
		powerState = true;

		// Play power up sound
		audio.PlayOneShot (voiceOver_1);

		// Show subtitles
		SubtitleManager.Instance.AddSubtitle ("Computer Systems are now powered up.", 1.0f);
		SubtitleManager.Instance.Play ();

		// Turn on computer screen
		computerScreen.TurnOnScreen ();
	}

	void Activate() {
		if(isActive) {
			return;
		}
		
		// Open Door
		animateDoor.OpenDoor ();

		// Play identification audio
		audio.PlayOneShot (voiceOver_2);

		// Show subtitles
		SubtitleManager.Instance.AddSubtitle ("User identified as Ben. Please proceed.", 2.0f);
		SubtitleManager.Instance.Play ();

		isActive = true;
	}
	
	void OnTriggerEnter(Collider other) {
		if(powerState && other.tag == "Hand") {
			Activate();
		}
	}
}
