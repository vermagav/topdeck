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
	
	void Awake()
	{
		audioSource = GetComponent<AudioSource> ();
		lastVolume = 0f;
	}
	
	void FixedUpdate ()
	{

		Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
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
