using UnityEngine;
using System.Collections;

public class LookAtControllerYRotation : MonoBehaviour {

	Quaternion lookRotation;
	Quaternion lookRotationDelta;

	private Vector3 point = new Vector3();
	private Vector3 targetVelocity = Vector3.forward;

	//public float rotationSpeed;
	//public float lookAngle;
	public bool XAxis, YAxis, ZAxis;
	public Transform anchor;

	void FixedUpdate()
	{
		RotateTorso();
	}

	void RotateTorso()
	{
		//Debug.Log (Input.GetAxis ("LookHorizontal"));
		
		Quaternion newLookRotation;
		
		if(point.magnitude == 0f)
		{
			newLookRotation = Quaternion.LookRotation(targetVelocity);
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

		//http://answers.unity3d.com/questions/35541/problem-finding-relative-rotation-from-one-quatern.html
		lookRotationDelta = Quaternion.Inverse(lookRotation) * newLookRotation;
		lookRotation = newLookRotation;

	}

	void UpdateVelocity(Vector3 movementDirection)
	{
		SetTargetVelocity(movementDirection);
	}

	void UpdateTorsoRotation(Vector3 rotateDirection)
	{
		SetPoint (rotateDirection);
	}

	public Quaternion GetLookRotation()
	{
		return lookRotation;
	}

	public Quaternion GetLookRotationDelta()
	{
		return lookRotationDelta;
	}

	public void SetPoint(Vector3 p)
	{
		point = p;
	}

	public void SetTargetVelocity(Vector3 v)
	{
		if(v.magnitude != 0f)
			targetVelocity = v;
	}

	public Vector3 GetTargetVelocity()
	{
		return targetVelocity;
	}
}
