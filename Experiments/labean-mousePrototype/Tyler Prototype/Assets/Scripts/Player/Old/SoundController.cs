using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	public float maxVolume;
	public float volumeScale;
	public bool X, Y, Z, onlyGrounded;

	private Vector3 oldPosition;
	private AudioSource audioSource;

	void Awake()
	{
		audioSource = GetComponent<AudioSource> ();
		oldPosition = transform.position;
	}

	void FixedUpdate()
	{
		Vector3 delta = transform.position - oldPosition;
		delta.x *= X ? 1 : 0;
		delta.y *= Y ? 1 : 0;
		delta.z *= Z ? 1 : 0;
	}
}
