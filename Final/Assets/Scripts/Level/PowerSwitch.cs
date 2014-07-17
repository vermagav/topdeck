using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PowerSwitch : MonoBehaviour {
	
	private bool isActive = false;

	public Light light;
	public float activeLightIntensity = 0.3f;

	public ComputerPanel computerPanel;

	public AnimateSwitch animateSwitch;
	public AudioClip switchSound;

	void Activate() {
		if(isActive) {
			return;
		}

		// Increase light intensity
		light.intensity = activeLightIntensity;

		// Animate switch
		animateSwitch.animateSwitch();

		// Play sound
		audio.PlayOneShot (switchSound);

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
