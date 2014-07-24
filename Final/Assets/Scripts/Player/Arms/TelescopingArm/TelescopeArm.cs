using UnityEngine;
using System.Collections;

public class TelescopeArm : BaseRobotArm {

	// private field assigned but not used.
	#pragma warning disable 0414 

	public float maxExtend = 5f;
	public GameObject hand;
	public GameObject piston;

	private ConfigurableJoint handJoint;
	private Vector3 desiredPosition;
	private Vector3 restingPosition;


	void Awake()
	{
		desiredPosition = new Vector3(0f, 0f, 0f);
		restingPosition = hand.transform.localPosition;
		handJoint = hand.GetComponent<ConfigurableJoint> ();
	}

	void FixedUpdate()
	{
		extendArm ();
	}
	
	private void extendArm()
	{
		//hand.transform.localPosition = restingPosition + desiredPosition;

		//hand.transform.localPosition = Vector3.Lerp (hand.transform.localPosition, restingPosition + desiredPosition, moveSpeed);
		handJoint.targetPosition = -(desiredPosition);
		piston.transform.localPosition = hand.transform.localPosition / 2f;
		piston.transform.localScale = new Vector3(0.3f, hand.transform.localPosition.z / 2f, 0.3f);//hand.transform.localPosition.z / 2f);
	}
	
	public override void SetArmState(bool state)
	{
		//send a message to Rob's hand implementation
	}
	
	public override void SetArmAxis(float axis)
	{
		//telescope the arm
		desiredPosition.z = axis * maxExtend;
		//Debug.Log (desiredPosition);
	}
}
