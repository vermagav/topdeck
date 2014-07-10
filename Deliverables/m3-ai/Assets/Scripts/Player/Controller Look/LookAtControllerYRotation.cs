using UnityEngine;
using System.Collections;

public class LookAtControllerYRotation : MonoBehaviour {

	Vector3 point = new Vector3();

	//public float rotationSpeed;
	//public float lookAngle;

	void FixedUpdate()
	{
		//Debug.Log (Input.GetAxis ("LookHorizontal"));

		Quaternion newLookRotation;

		if(point.magnitude == 0f)
		{
			newLookRotation = transform.parent.rotation;
		}
		else
		{
			newLookRotation = Quaternion.LookRotation(point);
		}

		//8888888888888888hingeJoint.spring.targetPosition = newLookRotation.eulerAngles.y;

		//newLookRotation = Quaternion.RotateTowards(transform.parent.rotation, newLookRotation, lookAngle);
		
		//transform.rotation = Quaternion.Lerp (transform.rotation, newLookRotation, rotationSpeed);

	}

	public void SetPoint(Vector3 p)
	{
		point = p;
	}
}
