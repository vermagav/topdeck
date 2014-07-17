using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class FenceController : MonoBehaviour {

	private Animator animator;

	public AudioClip fenceSound;

	void Awake() {
		animator = GetComponent<Animator> ();
	}

	public void setFenceUp(bool state) {
		animator.SetBool("FenceUp", state);

		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo (0);
		if(!audio.isPlaying && stateInfo.IsTag("Transition")) {
			audio.clip = fenceSound;
			audio.Play();
		}
	}
}
