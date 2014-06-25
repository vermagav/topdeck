using UnityEngine;
using System.Collections;

public class AnimateDoor : MonoBehaviour {

	JointMotor motor = new JointMotor();

	public void OpenDoor() {
		motor.force = 300;
		motor.targetVelocity = 90;
		hingeJoint.motor = this.motor;
	}

	public void CloseDoor() {
	}

}
