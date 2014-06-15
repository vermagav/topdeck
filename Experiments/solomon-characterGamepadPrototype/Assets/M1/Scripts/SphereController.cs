using UnityEngine;
using System.Collections;

[RequireComponent(typeof (CharacterController))]
public class SphereController : MonoBehaviour, ISphereController {

	public enum Mode
	{
		Light,
		Heavy
	}

	public Mode ControllerMode = Mode.Light;
	
	public Vector3 SphereMovementLocal;
	public Vector3 SphereMovementWorld
	{
		get { return transform.TransformDirection(SphereMovementLocal); }
	}

	public float MovementSpeed = 50f;
	public float TurningSpeed = 5f;

	float Acceleration = 0.01f;
	float HeavyAccelerationDampening = 0.2f;
	float DampeningFactor = 0.005f;
	
	public float RunFactor = 2.5f;
	public bool IsRunning = false;
	float currentMovementSpeed;

	public float MaxGroundSpeed = 0.3f;

	KeyCode KeyForward = KeyCode.W;
	KeyCode KeyReverse = KeyCode.S;
	KeyCode KeyLeft = KeyCode.A;
	KeyCode KeyRight = KeyCode.D;

	public CharacterController Controller;
	
	public Vector3 GetMovement() 
	{
		return SphereMovementLocal;
	}
	
	public Vector3 GetCurrentPosition() 
	{
		return transform.position;
	}

	public int GetControllerMode()
	{
		return (int)ControllerMode;
	}

	public float GetRotationSpeed()
	{
		switch (ControllerMode)
		{
		case Mode.Light:
			return 1;
		case Mode.Heavy:
			return HeavyAccelerationDampening;
		}
		Debug.LogError("Unexpected controller mode.");
		return 0;
	}
	
	// Use this for initialization
	void Awake () {
		SphereMovementLocal = new Vector3();
		Controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		CheckInput();
		ApplySphereMovement();
		ApplySphereDampening();
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		//ignore hits below us, as the character controller handles these
		if (hit.moveDirection.y < -0.3) 
			return;			

		//if we hit something from another direction, stop immediately
		float yDirection = SphereMovementLocal.y; //make sure it keeps falling instead of sticking to a wall in the air...
		SphereMovementLocal = new Vector3(0, yDirection, 0);

	}
	
	void CheckInput() {
		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown (KeyCode.RightShift))
		{
			IsRunning = true;
		}
		else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp (KeyCode.RightShift))
		{
			IsRunning = false;
		}

		UpdateMovementSpeed();

		if (Input.GetKey(KeyForward))
		{
			ApplyAnalogAcceleration(1);
			//ApplyAcceleration(transform.forward * currentMovementSpeed * Time.deltaTime);
		}
		else if (Input.GetKey(KeyReverse))
		{
			//transform.Translate(-transform.forward * Acceleration * Time.deltaTime);
			ApplyAnalogAcceleration(-1);
			//ApplyAcceleration(-transform.forward * currentMovementSpeed * Time.deltaTime);
		}
		
		if (Input.GetKey(KeyLeft))
		{
			ApplyAnalogRotation(-1);
		}
		if (Input.GetKey(KeyRight))
		{
			ApplyAnalogRotation(1);
		}

		if (Input.GetKey (KeyCode.Alpha1))
		{
			ControllerMode = Mode.Light;
		}
		if (Input.GetKey (KeyCode.Alpha2))
		{
			ControllerMode = Mode.Heavy;
		}
		
	}

	void UpdateMovementSpeed() {
		if (IsRunning)
		{
			currentMovementSpeed = MovementSpeed * RunFactor;
		}
		else
		{
			currentMovementSpeed = MovementSpeed;
		}
	}
	
	void ApplySphereMovement() {
		
		//Translate is in local space by default
		//SEE: http://answers.unity3d.com/questions/608928/moving-forward-in-world-space-with-rotation.html

		//transform.Translate(SphereMovementLocal, Space.World);

		switch (ControllerMode)
		{
			case Mode.Light:
				Controller.Move (SphereMovementLocal);
				break;
			case Mode.Heavy:
				Controller.Move (SphereMovementLocal * HeavyAccelerationDampening);
			break;
		}
		
		//TODO: if current sphere movement is in the same direction as the sphere direction, apply greater force
		
		//TODO: apply dampening if sphere collider is touching the ground
		
	}

	public void ApplyAnalogAcceleration(float direction)
	{
		ApplyAcceleration(Mathf.Clamp(direction, -1, 1) * transform.forward * currentMovementSpeed * Time.deltaTime);
	}

	public void ApplyAnalogRotation(float direction)
	{
		transform.Rotate(transform.up, TurningSpeed * Mathf.Clamp(direction, -1, 1));
	}
	
	void ApplyAcceleration(Vector3 direction) {
		Vector3 newSphereMovement = new Vector3 (SphereMovementLocal.x, SphereMovementLocal.y, SphereMovementLocal.z) + (direction * Acceleration);
		//float yDirection = SphereMovementLocal.y;

		//cap our maximum movement speed
		if (!IsRunning)
		{
			if (newSphereMovement.magnitude < MaxGroundSpeed)
				SphereMovementLocal = newSphereMovement;
		}
		else
		{
			if (newSphereMovement.magnitude < MaxGroundSpeed * RunFactor)
				SphereMovementLocal = newSphereMovement;
		}

		//SphereMovementLocal.y = yDirection;

		/*
		SphereMovementLocal.x = Mathf.Clamp (SphereMovementLocal.x, -MaxGroundSpeed, MaxGroundSpeed);
		SphereMovementLocal.z = Mathf.Clamp (SphereMovementLocal.z, -MaxGroundSpeed, MaxGroundSpeed);
		*/
	}
	
	void ApplySphereDampening() {
		SphereMovementLocal.x = DampenSphereAxis(SphereMovementLocal.x);
		SphereMovementLocal.z = DampenSphereAxis(SphereMovementLocal.z);
	}
	
	float DampenSphereAxis(float axisSpeed) {
		float dampenedSpeed = 0;
		if (axisSpeed < 0)
		{
			dampenedSpeed = axisSpeed + DampeningFactor * GetRotationSpeed();
			if (dampenedSpeed > 0)
				dampenedSpeed = 0;
		}
		else if (axisSpeed > 0)
		{
			dampenedSpeed = axisSpeed - DampeningFactor * GetRotationSpeed();
			if (dampenedSpeed < 0)
				dampenedSpeed = 0;
		}
		return dampenedSpeed;
	}

}
