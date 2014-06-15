using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour {


	private static SoundController instance = null;
	public static SoundController Instance
	{
		get
		{
			if (instance == null)
				instance = (SoundController)FindObjectOfType(typeof(SoundController));
			return instance;
		}
	}

	public AudioClip SolidCollide;
	public AudioClip SolidDrag;
	public AudioClip SandCollide;
	public AudioClip SandDrag;
	// ^ add more audio clips here

	public enum type {
		SolidCollide,
		SolidDrag,
		SandCollide,
		SandDrag
		// ^ add more corresponding enums here
	};
	
	Dictionary<type, AudioClip> library;
	
	void Start () {
		library = new Dictionary<type, AudioClip> ();
		// Make sure the enum and audioclip match
		library.Add (type.SolidCollide, SolidCollide);
		library.Add (type.SolidDrag, SolidDrag);
		library.Add (type.SandCollide, SandCollide);
		library.Add (type.SandDrag, SandDrag);
	}
	
	public void Play(type soundName, Vector3 position, float volume) {
		AudioSource.PlayClipAtPoint (library [soundName], position, volume);
	}
}
