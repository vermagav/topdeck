using UnityEngine;
using System.Collections;

public class JointSound : MonoBehaviour {

	public bool ChangeVolume;
	public bool ChangePitch;

	public float volumeRamp;
	public float volumeJerk;
	public float volumeMax;
	
	public float pitchRamp;
	public float pitchJerk;
	public float pitchMin;
	public float pitchMax;

	private AudioSource audioSource;
	private float lastVolume;
	private float nextVolume;
	private float lastAngle;

	private float lastPitch;
	private float nextPitch;

	void Awake()
	{
		audioSource = GetComponent<AudioSource> ();
		lastVolume = 0f;
		lastAngle = 0f;
		lastPitch = pitchMin;
	}
	
	void FixedUpdate ()
	{
		float angle = Quaternion.Angle (transform.rotation, transform.parent.rotation);
		float deltaAngle = angle - lastAngle;
		lastAngle = angle;

		if(ChangeVolume)
			deltaVolume (deltaAngle);

		if(ChangePitch)
			deltaPitch(deltaAngle);

	}

	private void deltaPitch(float deltaAngle)
	{
		nextPitch = Mathf.Clamp (Mathf.Pow (deltaAngle/90f, 2f) * pitchRamp, pitchMin, pitchMax);
		
		audioSource.pitch = Mathf.Lerp(lastPitch, nextPitch, pitchJerk);
		lastPitch = audioSource.pitch;
	}

	private void deltaVolume(float deltaAngle)
	{
		nextVolume = Mathf.Clamp (Mathf.Pow (deltaAngle/90f, 2f) * volumeRamp, 0, volumeMax);
		
		audioSource.volume = Mathf.Lerp(lastVolume, nextVolume, volumeJerk);
		lastVolume = audioSource.volume;
	}
}
