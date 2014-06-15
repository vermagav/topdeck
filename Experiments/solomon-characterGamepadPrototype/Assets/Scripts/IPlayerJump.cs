using UnityEngine;
using System.Collections;

public interface IPlayerJump {

	void InputPressed(float amount);
	void InputReleased(float amount);
}
