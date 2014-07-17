using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	private bool titlePlayed = false;
	public int titleIndex;
	public GUIController guiController;

	void OnTriggerEnter() {
		if (titlePlayed == false) {
			guiController.showBanner(titleIndex);
			titlePlayed = true;
		}
	}
}
