using UnityEngine;
using System.Collections;

public class AnimateSwitch : MonoBehaviour {

	private bool switchTriggered = false;
	
	public void animateSwitch() {
		// We only want the door to open once per OnTriggerStay()
		if (!switchTriggered) {
			animation.CrossFade("LeverActivate");
			switchTriggered = true;
		}
	}
}
