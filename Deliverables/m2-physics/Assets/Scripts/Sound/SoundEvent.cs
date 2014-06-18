using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class SoundEvent : MonoBehaviour {

	public enum Category
	{
		OneShot,
		Loop
	}

	public Category category = Category.OneShot;

	bool isRunning = false;

	//wrappers for attached AudioSource values
	public AudioClip clip { set {audio.clip = value;} }
	public float pitch { set {audio.pitch = value;} }
	public float volume { set {audio.volume = value;} }
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isRunning)
		{
			if (!audio.isPlaying)
			{
				Cleanup ();
			}
		}
	
	}

	public void Initialize (AudioClip clipToPlay, SoundEvent.Category soundCategory)
	{
		clip = clipToPlay;
		category = soundCategory;
		switch (category)
		{
		case Category.OneShot:
			//set one-shot properties here
			break;
		case Category.Loop:
			audio.loop = true;
			break;
		}

	}

	/// <summary>
	/// Used for one-shots that will not follow a moving transform (e.g. the player)
	/// </summary>
	/// <param name="position">World position.</param>
	public void SetPosition(Vector3 worldPos)
	{
		transform.position = worldPos;
	}

	/// <summary>
	/// GameObject transform to follow for loops. This must be set before playback begins.
	/// </summary>
	/// <param name="t">T.</param>
	public void SetFollowTransform(Transform t)
	{
		if (isRunning)
			return;
		transform.parent = t;
		transform.localPosition = new Vector3(); 
		//TODO: Allow an offset to be passed in (sounds at feet of robot instead of center, for instance)
	}

	public void Play()
	{
		audio.Play();
		isRunning = true;
	}

	public void Stop()
	{
		audio.Stop();
		Cleanup();
	}

	void Cleanup()
	{
		Destroy (this.gameObject);
	}
}
