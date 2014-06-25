using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour {

	[Range (0, 1)]
	public float robotVolume = 1;

	float loopSleepTime = 5.0f; //number of seconds before destroying a drag sound object that is no longer playing

	public List<SurfaceSoundData> surfaceData;
	Dictionary<Surface.Substance, SurfaceSoundData> surfaceDataDict;

	Dictionary<int, SoundEvent> loopSoundEventDict;

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

	void Start () {
		RebuildLibraryLookup(); //this will allow us to regenerate it in the future, possibly
	}

	public void Play(Surface.Substance substance, Surface.Cue cue, Vector3 position, float volume) {
		if (surfaceDataDict.ContainsKey(substance))
		{
			AudioSource.PlayClipAtPoint (surfaceDataDict[substance].GetSound(cue), position, volume);
		}
	}

	public void PlayOneShot(Surface.Substance substance, Surface.Cue cue, float volume, Vector3 position) 
	{
		if (surfaceDataDict.ContainsKey(substance))
		{
			GameObject go = new GameObject(substance + ":" + cue);
			SoundEvent se = go.AddComponent<SoundEvent>();
			se.Initialize(surfaceDataDict[substance].GetSound(cue), SoundEvent.Category.OneShot);
			se.SetPosition(position);
			se.volume = volume;
			se.Play ();
		}
	}
	
	public void PlayLoop(int soundID, Surface.Substance substance, Surface.Cue cue, float volume,
	                     Vector3 position = new Vector3(), Transform transformToFollow = null) 
	{
		if (loopSoundEventDict.ContainsKey(soundID))
		{
			//we've already created our loop, so merely update it with the current volume
			if (loopSoundEventDict[soundID] != null)
				loopSoundEventDict[soundID].volume = volume;
		}
		else
		{
			//Debug.Log("new loop created.");
			//TODO: Check the type of substance and only create a new AudioSource for new types of sounds

			//create entry for our looped sound so we can end it later
			GameObject go = new GameObject(substance + ":" + cue);
			SoundEvent se = go.AddComponent<SoundEvent>();
			se.Initialize(surfaceDataDict[substance].GetSound(cue), SoundEvent.Category.Loop);

			if (transformToFollow != null)
			{
				//if we are following a transform position, the position should be ignored
				se.SetFollowTransform(transformToFollow);
			}
			else
			{
				se.SetPosition(position);
			}
			se.volume = volume;
			loopSoundEventDict.Add (soundID, se);
			loopSoundEventDict[soundID].Play ();
		}
	}

	public void EndLoop(int soundID)
	{
		if (loopSoundEventDict.ContainsKey(soundID))
		{
			if (loopSoundEventDict[soundID] != null)
			{
				//Temporary fix... the value should never be null!
				loopSoundEventDict[soundID].Stop (); 
				//Debug.LogError (soundID);
			}
			loopSoundEventDict.Remove(soundID);
		}
	}

	public float GetLoopSleep()
	{
		return loopSleepTime;
	}

	void RebuildLibraryLookup () 
	{
		surfaceDataDict = new Dictionary<Surface.Substance, SurfaceSoundData>();
		foreach (SurfaceSoundData data in surfaceData)
		{
			data.ValidateClips();
			surfaceDataDict.Add (data.substance, data);
		}
		loopSoundEventDict = new Dictionary<int, SoundEvent>();
	}

	/// <summary>
	/// This should only run when in Editor Mode.
	/// </summary>
	public void RegenerateSurfaceProperties ()
	{
		List<SurfaceSoundData> oldData = surfaceData;
		surfaceData = new List<SurfaceSoundData>();

		for (int i=0; i < System.Enum.GetNames(typeof(Surface.Substance)).Length; i++)
		{
			SurfaceSoundData data = new SurfaceSoundData((Surface.Substance)i);
			for (int j=0; j < oldData.Count; j++)
			{
				//copy old data to the new one to avoid having to reassign sound clips if new fields are being added in code
				//check the type in the old data to the new one being made to assign clips back to the same type
				if (oldData[j].substance == data.substance)
				{
					data.sfxCollisionBank = oldData[j].sfxCollisionBank;
					data.sfxDraggingSound = oldData[j].sfxDraggingSound;
				}
			}

			surfaceData.Add (data);
		}

	}
}
