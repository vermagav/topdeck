using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour {

	public Texture buttonOff;
	public Texture buttonOn;

	public AnimateDoor animateDoor;
	
	void Update () {
	}

	void Awake() {
	}

	void OnTriggerStay (Collider Other) {
		// Change texture
		renderer.material.mainTexture = buttonOn;

		// Animate the door to open
		animateDoor.OpenDoor ();
	}

	void OnTriggerExit (Collider other) {
		// Change texture
		renderer.material.mainTexture = buttonOff;

		// Animate the door to close
		animateDoor.CloseDoor ();

	}
}
