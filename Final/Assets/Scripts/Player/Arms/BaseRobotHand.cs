using UnityEngine;
using System.Collections;

public abstract class BaseRobotHand : MonoBehaviour {

	Quaternion torsoRotation;
	Vector3 torsoVelocity;

	void Awake()
	{
		gameObject.tag = "Hand";
	}


	public abstract void UpdateInputAxis(float axis);

	public virtual void UpdateObjectRotation(Quaternion robotTorsoRotation)
	{
		torsoRotation = robotTorsoRotation;
	}

	public virtual void UpdateObjectPosition(Vector3 vel)
	{
		torsoVelocity = vel;
	}

	public Vector3 GetTorsoVelocityChange()
	{
		return torsoVelocity;
	}

	public Quaternion GetTorsoRotationState() 
	{
		return torsoRotation;
	}
}
