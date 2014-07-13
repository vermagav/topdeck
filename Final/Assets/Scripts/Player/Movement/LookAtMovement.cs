using UnityEngine;
using System.Collections;

public class LookAtMovement : MonoBehaviour {

	private Quaternion targetLookAt;
	private float lookAtSpeed = 0.2f;
	private Vector3 lastPos;

	void Awake()
	{
		lastPos = transform.position;
		targetLookAt = transform.rotation;
	}

	void FixedUpdate()
	{
		Vector3 deltaPos = transform.position - lastPos;
		lastPos = transform.position;

		deltaPos.y = 0;

		if(deltaPos.magnitude > 0.01f)
		{
			targetLookAt = Quaternion.LookRotation (deltaPos);
		}

		transform.rotation = Quaternion.Slerp (transform.rotation, targetLookAt, lookAtSpeed);

	}
}
