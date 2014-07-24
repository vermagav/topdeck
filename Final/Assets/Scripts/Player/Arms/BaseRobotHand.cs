using UnityEngine;
using System.Collections;

public abstract class BaseRobotHand : MonoBehaviour {

	void Awake()
	{
		gameObject.tag = "Hand";
	}


	public abstract void UpdateInputAxis(float axis);

}
