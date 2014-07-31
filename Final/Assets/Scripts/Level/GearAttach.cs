using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class GearAttach : MonoBehaviour {

	public GameObject targetGear;
	public AudioClip attachClip;
	public AudioClip happyRobot;

	public AnimateDoor animateDoor;

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.Equals(targetGear))
		{
			GetComponent<Animator>().SetBool("hasCenterGear", true);
			Destroy(other.gameObject);//.SetActive(false);
			animateDoor.OpenDoor ();
			AudioSource.PlayClipAtPoint(attachClip, transform.position);
			AudioSource.PlayClipAtPoint(happyRobot, transform.position);
		}
	}
}
