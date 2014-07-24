using UnityEngine;
using System.Collections;

public class ArmGrabber : BaseRobotArm {

	public BaseRobotHand hand;

	// Use this for initialization
	void Start () {
		hand = transform.FindChild ("Hand").GetComponent<BaseRobotHand>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Setup() {
		//hack to make it point down to check collision
		Vector3 rot = transform.localEulerAngles;
		rot.x = 50;
		transform.localEulerAngles = rot;
	}

	/// <summary>
	/// Translates bumper button input to arm action.
	/// </summary>
	/// <param name="state">Input state (pressed/released)</param>
	public override void SetArmState(bool state)
	{
		//hand.SendMessage("SetHandClosed", state, SendMessageOptions.DontRequireReceiver);
		//hand.SetHandClosed(state);
	}

	/// <summary>
	/// Translates analog trigger input to arm action.
	/// </summary>
	/// <param name="axis">Analog input axis.</param>
	public override void SetArmAxis(float axis)
	{
		hand.SendMessage("SetHandClosed", axis, SendMessageOptions.DontRequireReceiver);
	}

}
