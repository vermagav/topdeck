using UnityEngine;
using System.Collections;
using InControl;

[RequireComponent(typeof (SphereController))]
public class SphereControllerGamepadInput : MonoBehaviour {

	InputDevice Player1;

	SphereController Controller;
	SphereControllerJump JumpController;

	public bool GamePadPresent {
		get { return (Player1 != null); }
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
				Player1 = InputManager.Devices[0];
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

		if (Player1.LeftStick.Vector.magnitude > 0.1f) //accounting for slight drift
		{
			Controller.ApplyAnalogAcceleration(Player1.LeftStick.Y);
			Controller.ApplyAnalogRotation(Player1.LeftStick.X);
		}

		if (Player1.Action1.WasPressed)
			JumpController.InputPressed();
		if (Player1.Action1.WasReleased)
			JumpController.InputReleased();

		if (Player1.Action4.WasPressed)
		{
			int mode = (int)Controller.ControllerMode;
			mode++;
			int numModes = System.Enum.GetNames(typeof(SphereController.Mode)).Length;
			if (mode == numModes)
				mode = 0;
			Controller.ControllerMode = (SphereController.Mode)mode;
		}

		if (Player1.RightBumper.WasPressed)
			Controller.IsRunning = true;
		else if (Player1.RightBumper.WasReleased)
			Controller.IsRunning = false;

	}
}
