using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	private bool titlePlayed = false;

	void OnTriggerEnter() {
		if (titlePlayed == false) {
			// Title code goes here
			titlePlayed = true;
		}
	}
}
