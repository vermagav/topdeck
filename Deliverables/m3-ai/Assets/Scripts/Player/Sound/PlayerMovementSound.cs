using UnityEngine;
using System.Collections;

public class PlayerMovementSound : MonoBehaviour {
	
	public bool onlyGrounded;
	public float nonGroundedVolMult;
	public float volumeRamp;
	public float volumeJerk;

	private AudioSource audioSource;
	private float lastVolume;
	private float nextVolume;
	private bool grounded;

	public PlayerMovement playerMovement;
	
	void Awake()
	{
		audioSource = GetComponent<AudioSource> ();
		lastVolume = 0f;
	}
	
	void FixedUpdate ()
	{

		Vector3 targetVelocity = playerMovement.GetTargetVelocity();
		if(targetVelocity.magnitude > 1)
		{
			targetVelocity = targetVelocity / targetVelocity.magnitude;
		}

		if(!onlyGrounded || grounded)
		{
			nextVolume = Mathf.Pow (targetVelocity.magnitude, 2f) * volumeRamp;
		}
		else
		{
			nextVolume = Mathf.Pow (targetVelocity.magnitude, 2f) * volumeRamp * nonGroundedVolMult;
		}

		audioSource.volume = Mathf.Lerp(lastVolume, nextVolume, volumeJerk);
		lastVolume = audioSource.volume;

		audioSource.volume *= SoundController.Instance.robotVolume;

		grounded = false;
	}

	void OnCollisionEnter(Collision collision) {
		//moved collision sound code to collisiondata
	}

	void OnCollisionStay () {
		grounded = true;    
	}
}
