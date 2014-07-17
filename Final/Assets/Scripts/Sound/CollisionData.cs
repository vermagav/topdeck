using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollisionData : MonoBehaviour {

	public Surface.Substance substance;

	Dictionary<int, float> cleanupLoopDict;
	List<int> idCleanup; 

	//TODO(Rob): Add your movement modifiers here in a similar fashion

	void Start ()
	{
		cleanupLoopDict = new Dictionary<int, float>();
		idCleanup = new List<int>();
	}

	void Update ()
	{
		//UpdateLoopSleepTimes(); //TODO: Have the SoundController handle this!!!!!!!
	}

	/// <summary>
	/// Deducts time from each of the instantiated sounds from this object, removes them from play if the sleep time is exceeded.
	/// TODO: This should merely set them to be inactive probably to avoid creation/destruction costs, but this is easier to debug visually.
	/// </summary>
	void UpdateLoopSleepTimes()
	{
		if (cleanupLoopDict.Count > 0)
		{
			//see http://stackoverflow.com/questions/5235467/trying-to-extract-a-list-of-keys-from-a-net-dictionary
			List<int> keys = new List<int>(cleanupLoopDict.Keys);
			foreach (int key in keys)
			{
				cleanupLoopDict[key] -= Time.deltaTime;

				if (cleanupLoopDict[key] < 0)
				{
					idCleanup.Add (key);
				}
			
			}

			if (idCleanup.Count > 0)
			{
				foreach (int id in idCleanup)
				{
					SoundController.Instance.EndLoop(id);
					cleanupLoopDict.Remove (id);
				}
				idCleanup = new List<int>();
			}

		}
	}

	public Surface.Substance GetSubstance() {
		return substance;
	}

	void OnCollisionEnter(Collision collision) {

		//TODO: This could be shaped infinitely better, this is a very crude operation responding to velocity currently

		// Play the appropriate sound at the point of contact
		float colMagnitude = Mathf.Log10(collision.relativeVelocity.magnitude);
		//colMagnitude = (colMagnitude - 0.5f) * 10; //MAGIC NUMBERS

		float thresholdToPlay = 0.2f;

		if (colMagnitude > thresholdToPlay)
		{
			//Debug.Log ("Collision: " + collision.gameObject.name + " - Magnitude: " + colMagnitude);
			SoundController.Instance.PlayOneShot(GetSubstance(), Surface.Cue.Collide, colMagnitude, collision.contacts[0].point);
		}
	}
	
	void OnCollisionStay(Collision collision) {
		int soundPlaybackID = gameObject.GetInstanceID() * collision.gameObject.GetInstanceID();

		//TODO: This could be shaped infinitely better, this is a very crude operation responding to velocity currently
		
		// Play the appropriate sound at the point of contact
		Vector3 relativeVelocityXZ = collision.relativeVelocity;
		relativeVelocityXZ.y = 0; //filtering out gravity
		float colMagnitude = Mathf.Log10(relativeVelocityXZ.magnitude);
		
		float thresholdToPlay = 0.2f;

		if (colMagnitude < thresholdToPlay) //don't play or create an object if it won't be heard
		{
			colMagnitude = 0;
			
			//Debug.Log ("Drag: " + collision.gameObject.name + " - Magnitude: " + colMagnitude);
			
			//this needs to be called to actually lower the volume to zero. 
			//If the sound already exists, only the volume changes.
			SoundController.Instance.PlayLoop(soundPlaybackID, GetSubstance(), Surface.Cue.Drag, colMagnitude, 
			                                  this.transform.position, collision.transform);
		}
		else //play our dragging sound
		{
			//adding a reference to this ID as well as a sleep time... this lets us destroy objects that haven't been heard in a while
			
			if (!cleanupLoopDict.ContainsKey(soundPlaybackID))
			{
				cleanupLoopDict.Add (soundPlaybackID, SoundController.Instance.GetLoopSleep());
			}
			else
			{
				//HACK: to just destroy the drag sound each time to see if other code works

				//cleanupLoopDict[soundPlaybackID] = SoundController.Instance.GetLoopSleep();
			}
			
			SoundController.Instance.PlayLoop(soundPlaybackID, GetSubstance(), Surface.Cue.Drag, colMagnitude, 
			                                  this.transform.position, collision.transform);

			//Debug.Log ("Drag: " + collision.gameObject.name + " - Magnitude: " + colMagnitude);
		}

	}

	void OnCollisionExit(Collision collision)
	{
		int soundPlaybackID = gameObject.GetInstanceID() * collision.gameObject.GetInstanceID();
		SoundController.Instance.EndLoop(soundPlaybackID);
	}
	
}
