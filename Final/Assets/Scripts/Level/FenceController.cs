using UnityEngine;
using System.Collections;

public class FenceController : MonoBehaviour {

	private Animator animator;

	void Awake()
	{
		animator = GetComponent<Animator> ();
	}

	public void setFenceUp(bool state)
	{
		animator.SetBool("FenceUp", state);
	}
}
