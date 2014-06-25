using UnityEngine;
using System.Collections;

public class AnimateDoor : MonoBehaviour {

	private bool doorTriggered = false;

	public void OpenDoor() {
		// We only want the door to open
		if (!doorTriggered) {
			// Check for currently playing animation before playing
			if(animation.IsPlaying("CloseDoor")) {
				animation.CrossFade ("OpenDoor");
			} else {
				animation.Play ("OpenDoor");
			}
			doorTriggered = true;
		}
	}

	public void CloseDoor() {
		// Same as in OpenDoor()
		if(animation.IsPlaying("OpenDoor")) {
			animation.CrossFade ("CloseDoor");
		} else {
			animation.Play ("CloseDoor");
		}
		doorTriggered = false;
	}

}
