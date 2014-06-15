using UnityEngine;
using System.Collections;

public class LookAtControllerYRotation : MonoBehaviour {
	
	public float rotationSpeed;
	public float lookAngle;

	void FixedUpdate()
	{
		//Debug.Log (Input.GetAxis ("LookHorizontal"));

		Vector3 point = new Vector3 (Input.GetAxis ("LookHorizontal"), 0f, Input.GetAxis ("LookVertical"));

		Quaternion newLookRotation;

		if(point.magnitude == 0f)
		{
			newLookRotation = transform.parent.rotation;
		}
		else
		{
			newLookRotation = Quaternion.LookRotation(point);
		}

		newLookRotation = Quaternion.RotateTowards(transform.parent.rotation, newLookRotation, lookAngle);
		
		transform.rotation = Quaternion.Lerp (transform.rotation, newLookRotation, rotationSpeed);
	}
}
