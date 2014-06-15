using UnityEngine;
using System.Collections;
using InControl;

public class RobotPlayerInput : MonoBehaviour {

	InputDevice Gamepad;

	public bool GamePadPresent {
		get { return (Gamepad != null); }
	}

	public GameObject MotorRoot;

	IPlayerMotor Motor;
	IPlayerJump Jump;

	KeyCode KeyForward = KeyCode.W;
	KeyCode KeyReverse = KeyCode.S;
	KeyCode KeyLeft = KeyCode.A;
	KeyCode KeyRight = KeyCode.D;
	KeyCode KeyRun1 = KeyCode.LeftShift;
	KeyCode KeyRun2 = KeyCode.RightShift;

	KeyCode JumpKey = KeyCode.Space;

	void Awake() {
		InputManager.Setup();
	}

	// Use this for initialization
	void Start () {

		Motor = MotorRoot.GetComponent(typeof(IPlayerMotor)) as IPlayerMotor;
		Jump = MotorRoot.GetComponent(typeof(IPlayerJump)) as IPlayerJump;

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

	void Update() {
		InputManager.Update();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		PollKeyboardInput();
		PollGamepadInput();
	}

	void PollGamepadInput() {
		if (!GamePadPresent)
			return;
		
		if (Gamepad.LeftStick.Vector.magnitude > 0.1f) //accounting for slight drift
		{
			Motor.ApplyAcceleration(Gamepad.LeftStick.Y);
			Motor.ApplyRotation(Gamepad.LeftStick.X);
		}
		
		if (Gamepad.Action1.WasPressed)
			Jump.InputPressed(1);
		if (Gamepad.Action1.WasReleased)
			Jump.InputReleased(1);

		/*
		if (Gamepad.Action4.WasPressed)
		{
			int mode = (int)Controller.ControllerMode;
			mode++;
			int numModes = System.Enum.GetNames(typeof(SphereController.Mode)).Length;
			if (mode == numModes)
				mode = 0;
			Controller.ControllerMode = (SphereController.Mode)mode;
		}
		*/
		
		if (Gamepad.RightBumper.WasPressed)
			Motor.SetRunning(true);
		else if (Gamepad.RightBumper.WasReleased)
			Motor.SetRunning(false);
	}

	void PollKeyboardInput() {
		if (Input.GetKeyDown(KeyRun1) || Input.GetKeyDown (KeyRun2))
		{
			Motor.SetRunning(true);
		}
		else if (Input.GetKeyUp(KeyRun1) || Input.GetKeyUp (KeyRun2))
		{
			Motor.SetRunning(false);
		}

		if (Input.GetKey(KeyForward))
		{
			Motor.ApplyAcceleration(1);
		}
		else if (Input.GetKey(KeyReverse))
		{
			Motor.ApplyAcceleration(-1);
		}

		if (Input.GetKey(KeyLeft))
		{
			Motor.ApplyRotation(-1);
		}
		if (Input.GetKey(KeyRight))
		{
			Motor.ApplyRotation(1);
		}

		if (Input.GetKeyDown(JumpKey))
			Jump.InputPressed(1);
		if (Input.GetKeyUp(JumpKey))
			Jump.InputReleased(1);
	}
}
