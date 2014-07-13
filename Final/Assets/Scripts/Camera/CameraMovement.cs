using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	public Transform target;
	public float cameraLocationSpeed;

	private Vector3 viewingPos;

	void Awake()
	{
		viewingPos = transform.position - target.position;
	}

	void FixedUpdate()
	{
		transform.position = Vector3.Lerp (transform.position, target.position + viewingPos, cameraLocationSpeed);
		transform.LookAt (target.position);
	}
}
