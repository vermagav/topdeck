using UnityEngine;
using System.Collections;

public class ComputerPanel : MonoBehaviour {

	private bool powerState = false;
	private bool isActive = false;

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
		// TODO
		Debug.Log ("Computer Panel activated!");

		isActive = true;
	}
	
	void OnTriggerEnter(Collider other) {
		if(powerState && other.tag == "Player") {
			Activate();
		}
	}
}
