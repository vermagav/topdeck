using UnityEngine;
using System.Collections;

public class AnimateDoor : MonoBehaviour {

	private bool doorTriggered = false;

	public void OpenDoor() {
		// We only want the door to open
		if (!doorTriggered) {
			// Check for currently playing animation before queing
			// This prevents the robot from indefinitely queing multiple animations
			// by stepping on and off the button and using subsequent swings to escape
			if(animation.IsPlaying("CloseDoor")) {
				animation.PlayQueued ("OpenDoor");
			} else {
				animation.Play ("OpenDoor");
			}
			doorTriggered = true;
		}
	}

	public void CloseDoor() {
		// Same as in OpenDoor()
		if(animation.IsPlaying("OpenDoor")) {
			animation.PlayQueued("CloseDoor");
		} else {
			animation.PlayQueued ("CloseDoor");
		}
		doorTriggered = false;
	}

}
