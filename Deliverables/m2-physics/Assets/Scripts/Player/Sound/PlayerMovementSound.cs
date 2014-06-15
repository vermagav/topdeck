using UnityEngine;
using System.Collections;

[RequireComponent (typeof(PlayerMovement))]
public class PlayerMovementSound : MonoBehaviour {

	public bool onlyGrounded;
	public float nonGroundedVolMult;
	public float volumeRamp;
	public float volumeJerk;

	private AudioSource audioSource;
	private float lastVolume;
	private float nextVolume;
	private bool grounded;

	PlayerMovement playerMovement;
	
	void Awake()
	{
		audioSource = GetComponent<AudioSource> ();
		lastVolume = 0f;
		playerMovement = GetComponent<PlayerMovement>();
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

		grounded = false;
	}

	void OnCollisionStay () {
		grounded = true;    
	}
}
