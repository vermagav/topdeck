using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ArmGrabberHand))]
/// <summary>
/// This is now solely an animation view and does not perform any connections.
/// </summary>
public class ArmGrabberHandPinchers : BaseRobotHand {
	
	public GameObject leftPinchers;
	public GameObject rightPinchers;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	public override void SetHandClosed (float axis)
	{
		leftPinchers.transform.localEulerAngles = new Vector3(0, 45*(1 - axis), 0);
		rightPinchers.transform.localEulerAngles = new Vector3(0, -45*(1 - axis), 180);

	}

}
