using UnityEngine;
using System.Collections;

public class ComputerPanel : MonoBehaviour {

	private bool powerState = false;
	private bool isActive = false;

	public AnimateDoor animateDoor;

	public void PowerOn() {
		powerState = true;
		// TODO: power on sounds and effects, need to give feedback to player
		Debug.Log ("Computer Panel powered on...");
	}

	void Activate() {
		if(isActive) {
			return;
		}
		
		// Open Door
		animateDoor.OpenDoor ();

		// TODO:
		// 		activation sounds (computer panel, door opening)
		// 		activation visual effects
		// 		activation GUI text
		Debug.Log ("Computer Panel activated!");

		isActive = true;
	}
	
	void OnTriggerEnter(Collider other) {
		if(powerState && other.tag == "Player") {
			Activate();
		}
	}
}
