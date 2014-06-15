using UnityEngine;
using System.Collections;
using InControl;

[RequireComponent(typeof (SphereController))]
public class SphereControllerGamepadInput : MonoBehaviour {

	InputDevice Gamepad;

	SphereController Controller;
	SphereControllerJump JumpController;

	public bool GamePadPresent {
		get { return (Gamepad != null); }
	}

	// Use this for initialization
	void Start () {
		InputManager.Setup();
		Controller = GetComponent<SphereController>();
		JumpController = GetComponent<SphereControllerJump>();

		for (int i=0; i < InputManager.Devices.Count; i++)
		{
			switch (i)
			{
			case 0:
				Gamepad = InputManager.Devices[0];
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		InputManager.Update();
	}

	void FixedUpdate () {
		CheckInput();
	}

	void CheckInput() {
		if (!GamePadPresent)
			return;

		if (Gamepad.LeftStick.Vector.magnitude > 0.1f) //accounting for slight drift
		{
			Controller.ApplyAnalogAcceleration(Gamepad.LeftStick.Y);
			Controller.ApplyAnalogRotation(Gamepad.LeftStick.X);
		}

		if (Gamepad.Action1.WasPressed)
			JumpController.InputPressed();
		if (Gamepad.Action1.WasReleased)
			JumpController.InputReleased();

		if (Gamepad.Action4.WasPressed)
		{
			int mode = (int)Controller.ControllerMode;
			mode++;
			int numModes = System.Enum.GetNames(typeof(SphereController.Mode)).Length;
			if (mode == numModes)
				mode = 0;
			Controller.ControllerMode = (SphereController.Mode)mode;
		}

		if (Gamepad.RightBumper.WasPressed)
			Controller.IsRunning = true;
		else if (Gamepad.RightBumper.WasReleased)
			Controller.IsRunning = false;

	}
}
