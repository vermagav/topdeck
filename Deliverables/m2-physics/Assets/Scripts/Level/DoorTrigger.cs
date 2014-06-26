using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {

	public Texture buttonOff;
	public Texture buttonOn;

	public AnimateDoor animateDoor;

	bool triggerActive = false;
	
	void Update () {
	}

	void Awake() {
	}

	void OnCollisionStay () {
		// Change texture
		renderer.material.mainTexture = buttonOn;

		// Animate the door to open
		animateDoor.OpenDoor ();

		triggerActive = true;
	}

	void OnCollisionExit () {
		// Change texture
		renderer.material.mainTexture = buttonOff;

		// Animate the door to close
		animateDoor.CloseDoor ();

		triggerActive = false;
	}

	public bool GetTriggerState() 
	{
		return triggerActive;
	}
}
