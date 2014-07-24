using UnityEngine;
using System.Collections;

public abstract class BaseRobotArm : MonoBehaviour {

	public abstract void SetArmState(bool state);
	public abstract void SetArmAxis(float axis);
}
	

