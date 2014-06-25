using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ArmGrabberHand))]
[RequireComponent (typeof(AudioSource))]
public class ArmGrabberHandSounds : MonoBehaviour {

	public AudioClip sfxOpen;
	public AudioClip sfxClose;

	ArmGrabberHand hand;

	bool lastIsOpen = false;

	// Use this for initialization
	void Start () {
		hand = GetComponent<ArmGrabberHand>();
		lastIsOpen = hand.isOpen;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (lastIsOpen != hand.isOpen)
		{
			lastIsOpen = hand.isOpen;
			if (hand.isOpen)
			{
				//play hand opening sound
				if (sfxOpen != null)
				{
					audio.clip = sfxOpen;
					audio.Play ();
				}

			}
			else
			{
				//play hand closing sound
				if (sfxClose != null)
				{
					audio.clip = sfxClose;
					audio.Play ();
				}
			}
		}
	
	}
}
