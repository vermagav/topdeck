using UnityEngine;
using System.Collections;

public abstract class BaseRobotArm : MonoBehaviour {

	public BaseRobotHand hand;

	Quaternion torsoRotation;
	Vector3 torsoVelocity;

	public abstract void SetArmState(bool state);
	public abstract void SetArmAxis(float axis);
	public void SetTorsoRotationState(Quaternion robotTorsoRotation)
	{
		torsoRotation = robotTorsoRotation;
	}

	public void SetTorsoVelocityChange (Vector3 vel)
	{
		torsoVelocity = vel;
	}

	public Quaternion GetTorsoRotationState() 
	{
		return torsoRotation;
	}

	public Vector3 GetTorsoVelocityChange()
	{
		return torsoVelocity;
	}


}
	

