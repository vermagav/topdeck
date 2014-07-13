using UnityEngine;
using System.Collections;

public class SpawnData {
	private string spawnTag;
	private string displayText;
	private AudioClip audioClip;

	// Constructor
	public SpawnData(string tag, string text, AudioClip clip) {
		spawnTag = tag;
		displayText = text;
		audioClip = clip;
	}

	// Access functions
	public string SpawnTag() {
		return spawnTag;
	}
	public string DisplayText() {
		return displayText;
	}
	public AudioClip AudioClip() {
		return audioClip;
	}
}
