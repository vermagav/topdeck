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

	void OnTriggerStay (Collider Other) {
		// Change texture
		renderer.material.mainTexture = buttonOn;

		// Animate the door to open
		animateDoor.OpenDoor ();

		triggerActive = true;
	}

	void OnTriggerExit (Collider other) {
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
