using UnityEngine;
using System.Collections;

public class RobotM1Motor : MonoBehaviour, IPlayerMotor {

	public enum MovementType
	{
		Light,
		Heavy
	}
	
	public MovementType Mode = MovementType.Light;
	
	public Vector3 VelocityLocal;
	public Vector3 VelocityWorld
	{
		get { return transform.TransformDirection(VelocityLocal); }
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

	public CharacterController Controller;
	
	public Vector3 GetMovement() 
	{
		return VelocityLocal;
	}
	
	public Vector3 GetCurrentPosition() 
	{
		return transform.position;
	}
	
	public int GetControllerMode()
	{
		return (int)Mode;
	}
	
	public float GetRotationSpeed()
	{
		switch (Mode)
		{
		case MovementType.Light:
			return 1;
		case MovementType.Heavy:
			return HeavyAccelerationDampening;
		}
		Debug.LogError("Unexpected controller mode.");
		return 0;
	}

	public void SetVerticalSpeed(float verticalSpeed)
	{
		VelocityLocal.y = verticalSpeed;
	}

	public bool ControllerIsGrounded()
	{
		return Controller.isGrounded;
	}
	
	// Use this for initialization
	void Awake () {
		VelocityLocal = new Vector3();
		Controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		UpdateMovementSpeed();
		ApplyMovement();
		ApplyMovementDampening();
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit) {
		//ignore hits below us, as the character controller handles these
		if (hit.moveDirection.y < -0.3) 
			return;			
		
		//if we hit something from another direction, stop immediately
		float yDirection = VelocityLocal.y; //make sure it keeps falling instead of sticking to a wall in the air...
		VelocityLocal = new Vector3(0, yDirection, 0);
		
	}
	
	void CheckInput() {

		if (Input.GetKey (KeyCode.Alpha1))
		{
			Mode = MovementType.Light;
		}
		if (Input.GetKey (KeyCode.Alpha2))
		{
			Mode = MovementType.Heavy;
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
	
	void ApplyMovement() {
		
		//Translate is in local space by default
		//SEE: http://answers.unity3d.com/questions/608928/moving-forward-in-world-space-with-rotation.html
		
		//transform.Translate(SphereMovementLocal, Space.World);
		
		switch (Mode)
		{
		case MovementType.Light:
			Controller.Move (VelocityLocal);
			break;
		case MovementType.Heavy:
			Controller.Move (VelocityLocal * HeavyAccelerationDampening);
			break;
		}
		
		//TODO: if current sphere movement is in the same direction as the sphere direction, apply greater force
		
		//TODO: apply dampening if sphere collider is touching the ground
		
	}

	public void SetRunning(bool state)
	{
		IsRunning = state;
	}
	
	public void ApplyRotation(float direction)
	{
		transform.Rotate(transform.up, TurningSpeed * Mathf.Clamp(direction, -1, 1));
	}
	
	public void ApplyAcceleration(float direction) {

		//convert inputDirection to a movement vector
		Vector3 movementVector = Mathf.Clamp(direction, -1, 1) * transform.forward * currentMovementSpeed * Time.deltaTime;

		Vector3 updatedVelocity = new Vector3 (VelocityLocal.x, VelocityLocal.y, VelocityLocal.z) + (movementVector * Acceleration);
		
		//cap our maximum movement speed
		if (!IsRunning)
		{
			if (updatedVelocity.magnitude < MaxGroundSpeed)
				VelocityLocal = updatedVelocity;
		}
		else
		{
			if (updatedVelocity.magnitude < MaxGroundSpeed * RunFactor)
				VelocityLocal = updatedVelocity;
		}
	
	}
	
	void ApplyMovementDampening() {
		VelocityLocal.x = DampenMovementDirection(VelocityLocal.x);
		VelocityLocal.z = DampenMovementDirection(VelocityLocal.z);
	}
	
	float DampenMovementDirection(float axisSpeed) {
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
