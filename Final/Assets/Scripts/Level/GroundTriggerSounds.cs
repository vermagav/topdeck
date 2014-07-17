using UnityEngine;
using System.Collections;

[RequireComponent (typeof(GroundTrigger))]
[RequireComponent (typeof(AudioSource))]
public class GroundTriggerSounds : MonoBehaviour {

	public AudioClip sfxActivated;
	public AudioClip sfxDeactivated;

	GroundTrigger trigger;
	bool lastTriggerState;

	// Use this for initialization
	void Start () {
		trigger = GetComponent<GroundTrigger>();
		lastTriggerState = trigger.GetTriggerState();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (lastTriggerState != trigger.GetTriggerState())
		{
			lastTriggerState = trigger.GetTriggerState();
			if (lastTriggerState)
			{
				//play open
				if (sfxActivated != null)
				{
					audio.clip = sfxActivated;
				}
			}
			else
			{
				//play close
				if (sfxDeactivated != null)
				{
					audio.clip = sfxDeactivated;
				}
			}
			audio.Play ();
		}
	
	}
}
