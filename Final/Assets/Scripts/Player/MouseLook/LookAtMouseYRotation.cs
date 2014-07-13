using UnityEngine;
using System.Collections;

public class LookAtMouseYRotation : MonoBehaviour {

	public float rotationSpeed;
	public float lookAngle;

	void FixedUpdate()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		RaycastHit hit;
		
		var layerMask = 1 << 8;
		layerMask = ~layerMask;
		
		if (Physics.Raycast(ray, out hit, 100, layerMask))
		{
			Vector3 point = hit.point;
			point.y = transform.position.y;

			Quaternion newLookRotation = Quaternion.LookRotation(point - transform.position);

			newLookRotation = Quaternion.RotateTowards(transform.parent.rotation, newLookRotation, lookAngle);
			
			transform.rotation = Quaternion.Lerp (transform.rotation, newLookRotation, rotationSpeed);
		}
	}
}