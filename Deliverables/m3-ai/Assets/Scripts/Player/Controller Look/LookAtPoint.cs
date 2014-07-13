using UnityEngine;
using System.Collections;

public class LookAtPoint : MonoBehaviour {

	private Vector3 point = new Vector3();

	//public float rotationSpeed;
	//public float lookAngle;
	public bool XAxis, YAxis, ZAxis;

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
		JointSpring newSpring = hingeJoint.spring;
		if(XAxis)
		{
			newSpring.targetPosition = newLookRotation.eulerAngles.x;
		}
		else if(YAxis)
		{
			newSpring.targetPosition = newLookRotation.eulerAngles.y;
		}
		else if(ZAxis)
		{
			newSpring.targetPosition = newLookRotation.eulerAngles.z;
		}
		hingeJoint.spring = newSpring;

		//newLookRotation = Quaternion.RotateTowards(transform.parent.rotation, newLookRotation, lookAngle);
		
		//transform.rotation = Quaternion.Lerp (transform.rotation, newLookRotation, rotationSpeed);

	}

	public void SetPoint(Vector3 p)
	{
		point = p;
	}
}
