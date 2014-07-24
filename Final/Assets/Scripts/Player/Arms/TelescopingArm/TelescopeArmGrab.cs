using UnityEngine;
using System.Collections;

public class TelescopeArmGrab : MonoBehaviour, IArm {

	// private field assigned but not used.
	#pragma warning disable 0414 

	public ArmGrabberHand hand;

	public float maxExtend = 5f;
	public GameObject piston;

	public GameObject wrist;
	
	private ConfigurableJoint wristJoint;
	private Vector3 desiredPosition;
	private Vector3 restingPosition;
	
	
	void Awake()
	{
		desiredPosition = new Vector3(0f, 0f, 0f);
		restingPosition = wrist.transform.localPosition;
		wristJoint = wrist.GetComponent<ConfigurableJoint> ();
		hand = wrist.transform.FindChild ("Hand").GetComponent<ArmGrabberHand>();

	}
	
	void FixedUpdate()
	{
		extendArm ();
	}

	public void Setup() {
		//any setup code for the arm after being attached can be put here
	}
	
	private void extendArm()
	{
		//hand.transform.localPosition = restingPosition + desiredPosition;
		
		//hand.transform.localPosition = Vector3.Lerp (hand.transform.localPosition, restingPosition + desiredPosition, moveSpeed);
		wristJoint.targetPosition = -(desiredPosition);
		piston.transform.localPosition = wrist.transform.localPosition / 2f;
		piston.transform.localScale = new Vector3(0.3f, wrist.transform.localPosition.z / 2f, 0.3f);//hand.transform.localPosition.z / 2f);
	}
	
	public void SetArmState(bool state)
	{
		//send a message to Rob's hand implementation
		//hand.SetHandClosed(state);
	}
	
	public void SetArmAxis(float axis)
	{
		//telescope the arm
		desiredPosition.z = axis * maxExtend;
		//Debug.Log (desiredPosition);
	}
}
