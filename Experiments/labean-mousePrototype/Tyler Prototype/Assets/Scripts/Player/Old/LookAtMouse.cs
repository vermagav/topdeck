using UnityEngine;
using System.Collections;

public class LookAtMouse : MonoBehaviour {

	public Transform camera;
	public float rotationSpeed;

	void FixedUpdate()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;

		var layerMask = 1 << 8;
		layerMask = ~layerMask;

		if (Physics.Raycast(ray, out hit, 100, layerMask))
		{

			Quaternion newLookRotation = Quaternion.LookRotation(hit.point - transform.position);

			transform.rotation = Quaternion.Lerp (transform.rotation, newLookRotation, rotationSpeed);
		}
	}
}
