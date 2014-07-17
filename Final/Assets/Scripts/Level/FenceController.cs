using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class FenceController : MonoBehaviour {

	private Animator animator;
	
	void Awake() {
		animator = GetComponent<Animator> ();
	}

	public void setFenceUp(bool state) {
		animator.SetBool("FenceUp", state);
	}
}
