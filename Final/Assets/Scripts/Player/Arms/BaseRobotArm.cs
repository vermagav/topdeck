using UnityEngine;
using System.Collections;

public abstract class BaseRobotArm : MonoBehaviour {

	public BaseRobotHand hand;

	Quaternion torsoRotation;

	public abstract void SetArmState(bool state);
	public abstract void SetArmAxis(float axis);
	public void SetTorsoRotationState(Quaternion robotTorsoRotation)
	{
		torsoRotation = robotTorsoRotation;
	}

	public Quaternion GetTorsoRotationState() 
	{
		return torsoRotation;
	}

}
	

