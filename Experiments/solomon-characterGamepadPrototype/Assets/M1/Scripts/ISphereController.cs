using UnityEngine;
using System.Collections;

public interface ISphereController {

	Vector3 GetMovement();
	Vector3 GetCurrentPosition();
	int GetControllerMode();
	float GetRotationSpeed();
}
