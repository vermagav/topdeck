using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable] //allows us to see these properties in the editor
public class SurfaceSoundData : System.Object {

	public Surface.Substance substance;

	public List<AudioClip> sfxCollisionBank; //array allows for sound bank behavior later
	public AudioClip sfxDraggingSound; //we'll use only one for now

	int lastCollisionSFXIndex = -1; //makes the randomization work, see GetCollisionSound()

	public SurfaceSoundData(Surface.Substance type) {
		substance = type;
		sfxCollisionBank = new List<AudioClip>();
	}

	/// <summary>
	/// Run this to remove null fields from Lists of sounds (e.g. sound banks)
	/// (in case someone added clips in the editor that are still null
	/// </summary>
	public void ValidateClips()
	{
		//TODO make this a generic function that can handle any sort of sound bank
		List<AudioClip> clips = new List<AudioClip>();
		for (int i=0; i < sfxCollisionBank.Count; i++)
		{
			if (sfxCollisionBank[i] != null)
			{
				clips.Add (sfxCollisionBank[i]);
			}
		}
		sfxCollisionBank = clips;
	}

	public AudioClip GetSound(Surface.Cue cue)
	{
		switch (cue)
		{
		case Surface.Cue.Collide:
			return GetCollisionSound();
		case Surface.Cue.Drag:
			return GetDraggingSound();
		}
		Debug.LogWarning("Sound cue ID (" + cue + ") not found on " + substance + ".");
		return new AudioClip();

	}

	public AudioClip GetDraggingSound() {
		if (sfxDraggingSound != null)
			return sfxDraggingSound;
		else
		{
			Debug.LogWarning("No dragging sound cue(s) present for " + substance + ".");
			return new AudioClip();
		}
	}

	public AudioClip GetCollisionSound() {
		//check to see if we have any sounds
		if (sfxCollisionBank == null || sfxCollisionBank.Count == 0)
		{
			Debug.LogWarning("No collision sound cue(s) present for " + substance + ".");
			return new AudioClip(); //empty clip
		}

		//if we have only one sound in our array, return it
		if (sfxCollisionBank.Count == 1)
		{
			return sfxCollisionBank[0];
		}

		//if we have multiple sounds, return a random one (that is not the last one we chose)
		//note in Random.Range that the MAX value is never returned
		int sfxIndex = Random.Range (0, sfxCollisionBank.Count);
		if (sfxIndex == lastCollisionSFXIndex)
		{
			sfxIndex++; //simple way to avoid repeating, just go to the next element in the array

			if (sfxIndex == sfxCollisionBank.Count)
			{
				sfxIndex = 0;
			}
		}
		lastCollisionSFXIndex = sfxIndex; //store the one we chose so we avoid repetition

		//Debug.Log ("Collision (" + substance + ") at index " + sfxIndex);

		return sfxCollisionBank[sfxIndex];

	}

}


