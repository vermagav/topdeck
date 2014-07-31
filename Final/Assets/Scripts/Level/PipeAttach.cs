using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class PipeAttach : MonoBehaviour {
	
	public GameObject targetPipe;
	public AudioClip attachClip;
	public AudioClip happyRobot;
	
	public FenceController fenceController;
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.Equals(targetPipe))
		{
			GetComponent<Animator>().SetBool("hasPipe", true);
			other.gameObject.SetActive(false);
			fenceController.setFenceUp(false);
			AudioSource.PlayClipAtPoint(attachClip, transform.position);
			AudioSource.PlayClipAtPoint(happyRobot, transform.position);
		}
	}
}
