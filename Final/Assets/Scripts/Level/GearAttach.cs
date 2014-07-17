using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class GearAttach : MonoBehaviour {

	public GameObject targetGear;

	public AnimateDoor animateDoor;

	public AudioClip cogsAudio;

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.Equals(targetGear))
		{
			GetComponent<Animator>().SetBool("hasCenterGear", true);
			other.gameObject.SetActive(false);
			animateDoor.OpenDoor ();
			audio.clip = cogsAudio;
			audio.Play();
		}
	}
}
