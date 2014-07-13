using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AnimateDoor))]
public class AnimateDoorSounds : MonoBehaviour {

	//THIS SCRIPT SUCKS THROW IT AWAY

	public AudioClip sfxOpen;
	public AudioClip sfxClose;

	AnimateDoor door;

	bool doorIsOpening = false;
	bool doorIsClosing = false; //not very elegant, hacky

	// Use this for initialization
	void Start () {
		door = GetComponent<AnimateDoor>();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (door.animation.isPlaying)
		{
			if (door.animation.IsPlaying("CloseDoor"))
			{
				doorIsClosing = true;
			}
			else //door is opening
			{
				if (!doorIsOpening)
				{
					doorIsOpening = true;
					//play opening sound
					if (sfxOpen != null)
					{
						audio.clip = sfxOpen;
						audio.Play ();
					}
				}
			}
		}
		else
		{
			doorIsOpening = false;
			if (doorIsClosing)
			{
				//play closing sound
				doorIsClosing = false;
				if (sfxClose != null)
				{
					audio.clip = sfxClose;
					audio.Play ();
				}
			}
		}
	
	}
}
