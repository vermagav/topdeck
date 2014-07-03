using UnityEngine;
using System.Collections;

public class AnimateDoor : MonoBehaviour {

	private bool doorTriggered = false;

	public void OpenDoor() {
		// We only want the door to open once per OnTriggerStay()
		if (!doorTriggered) {
			animation.CrossFade("OpenDoor");
			doorTriggered = true;
		}
	}

	public void CloseDoor() {
		animation.CrossFade ("CloseDoor");
		doorTriggered = false;
	}

}
