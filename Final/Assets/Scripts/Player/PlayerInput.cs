using UnityEngine;
using System.Collections;
using InControl;

[RequireComponent (typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour {

	public PlayerMovement playerMovement;
	public LookAtControllerYRotation lookAtControllerYRotation;
	public PlayerArmSocket arm;

	InputDevice Gamepad;
	
	public bool GamePadPresent {
		get { return (Gamepad != null); }
	}

	// Use this for initialization
	void Start () {
		for (int i=0; i < InputManager.Devices.Count; i++)
		{
			switch (i)
			{
			case 0:
				Gamepad = InputManager.Devices[0];
				break;
			}
		}
		if (playerMovement == null)
			playerMovement = GetComponent<PlayerMovement>();
		if (lookAtControllerYRotation == null)
		{
			//find the Torso and get that component, which we don't need right now
			//make sure it is assigned in the editor!!
		}
	}
	
	// Update is called once per frame
	void Update () {
		//InputManager.Update(); //this is done by GameController.cs now
		PollGamepadInput();
		PollKeyboardInput();

	}

	void PollGamepadInput() {
		if (!GamePadPresent)
			return;

		float bias = 0.5f;

		//now check inputs and assign them

		//Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

		playerMovement.SetTargetVelocity(new Vector3(Gamepad.LeftStick.X * bias, 0, Gamepad.LeftStick.Y * bias));

		//Vector3 point = new Vector3 (Input.GetAxis ("LookHorizontal"), 0f, Input.GetAxis ("LookVertical"));

		lookAtControllerYRotation.SetPoint(new Vector3(Gamepad.RightStick.X, 0, Gamepad.RightStick.Y));

		//Input.GetButton("Fire2")

		arm.SendMessage("SetArmState", Gamepad.RightBumper.State, SendMessageOptions.DontRequireReceiver);
		arm.SendMessage("SetArmAxis", Gamepad.RightTrigger.Value, SendMessageOptions.DontRequireReceiver);
	}

	/// <summary>
	/// Checks keyboard input and moves the character.
	/// TODO: Make this work with the mouse to aim.
	/// </summary>
	void PollKeyboardInput() {
		// Process keyboard input for movement only if GamePad is NOT present
		if (!GamePadPresent) {
			float xMovement = 0;
			float yMovement = 0;

			if (Input.GetKey(KeyCode.W)) {
				xMovement = 1;
			}
			else if (Input.GetKey(KeyCode.S)) {
				xMovement = -1;
			}
			else if (Input.GetKey(KeyCode.A)) {
				yMovement = -1;
			}
			else if (Input.GetKey(KeyCode.D)){
				yMovement = 1;
			}

			playerMovement.SetTargetVelocity(new Vector3(yMovement, 0, xMovement));
		}
	}
}
