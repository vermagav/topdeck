using UnityEngine;
using System.Collections;

public class PowerSwitch : MonoBehaviour {
	
	private bool isActive = false;

	public Light light;
	public float activeLightIntensity = 0.3f;

	public ComputerPanel computerPanel;

	void Activate() {
		if(isActive) {
			return;
		}

		// Increase light intensity
		light.intensity = activeLightIntensity;

		// Power on computer panel
		computerPanel.PowerOn();

		isActive = true;
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			Activate();
		}
	}
}
