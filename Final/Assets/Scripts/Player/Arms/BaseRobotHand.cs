using UnityEngine;
using System.Collections;

public abstract class BaseRobotHand : MonoBehaviour {

	Quaternion torsoRotation;

	void Awake()
	{
		gameObject.tag = "Hand";
	}


	public abstract void UpdateInputAxis(float axis);

	public virtual void UpdateObjectRotation(Quaternion robotTorsoRotation)
	{
		torsoRotation = robotTorsoRotation;
	}

	public Quaternion GetTorsoRotationState() 
	{
		return torsoRotation;
	}
}
