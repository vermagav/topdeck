using UnityEngine;
using System.Collections;

public class SphereControllerSimple : MonoBehaviour, ISphereController {

	public Vector3 SphereMovementLocal;
	public Vector3 SphereMovementWorld
	{
		get { return transform.TransformDirection(SphereMovementLocal); }
	}
	
	public float MovementSpeed = 50f;
	public float TurningSpeed = 5f;
	
	float Acceleration = 0.01f;
	float DampeningFactor = 0.005f;
	
	public float MaxGroundSpeed = 0.3f;
	
	KeyCode KeyForward = KeyCode.W;
	KeyCode KeyReverse = KeyCode.S;
	KeyCode KeyLeft = KeyCode.A;
	KeyCode KeyRight = KeyCode.D;

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
		return -1;
	}

	public float GetRotationSpeed()
	{
		return 1;
	}

	// Use this for initialization
	void Start () {
		SphereMovementLocal = new Vector3();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		CheckInput();
		ApplySphereMovement();
		ApplySphereDampening();
	}

	void CheckInput() {
		if (Input.GetKey(KeyForward))
		{
			ApplyAcceleration(transform.forward * MovementSpeed * Time.deltaTime);
		}
		else if (Input.GetKey(KeyReverse))
		{
			//transform.Translate(-transform.forward * Acceleration * Time.deltaTime);
			ApplyAcceleration(-transform.forward * MovementSpeed * Time.deltaTime);
		}
	
		if (Input.GetKey(KeyLeft))
		{
			transform.Rotate(transform.up, -TurningSpeed);
		}
		if (Input.GetKey(KeyRight))
		{
			transform.Rotate (transform.up, TurningSpeed);
		}

	}

	void ApplySphereMovement() {

		//Translate is in local space by default
		//SEE: http://answers.unity3d.com/questions/608928/moving-forward-in-world-space-with-rotation.html

		transform.Translate(SphereMovementLocal, Space.World);

		//TODO: if current sphere movement is in the same direction as the sphere direction, apply greater force

		//TODO: apply dampening if sphere collider is touching the ground

	}

	void ApplyAcceleration(Vector3 direction) {
		Vector3 newSphereMovement = SphereMovementLocal + (direction * Acceleration);
		
		//cap our maximum movement speed
		if (newSphereMovement.magnitude < MaxGroundSpeed)
			SphereMovementLocal = newSphereMovement;

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
			dampenedSpeed = axisSpeed + DampeningFactor;
			if (dampenedSpeed > 0)
				dampenedSpeed = 0;
		}
		else if (axisSpeed > 0)
		{
			dampenedSpeed = axisSpeed - DampeningFactor;
			if (dampenedSpeed < 0)
				dampenedSpeed = 0;
		}
		return dampenedSpeed;
	}


}
