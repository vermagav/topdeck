using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour {

	public List<SurfaceSoundData> surfaceData;
	Dictionary<Surface.Substance, SurfaceSoundData> surfaceDataDict;

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

	void RebuildLibraryLookup () 
	{
		surfaceDataDict = new Dictionary<Surface.Substance, SurfaceSoundData>();
		foreach (SurfaceSoundData data in surfaceData)
		{
			data.ValidateClips();
			surfaceDataDict.Add (data.substance, data);
		}

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
