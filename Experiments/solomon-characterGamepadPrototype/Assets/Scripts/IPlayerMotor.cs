using UnityEngine;
using System.Collections;

public interface IPlayerMotor {

	void ApplyRotation(float direction);
	void ApplyAcceleration(float direction);

	void SetRunning(bool state);

	void SetVerticalSpeed(float verticalSpeed);

	bool ControllerIsGrounded();

	Vector3 GetMovement();
	Vector3 GetCurrentPosition();
	int GetControllerMode();
	float GetRotationSpeed();
	
}
