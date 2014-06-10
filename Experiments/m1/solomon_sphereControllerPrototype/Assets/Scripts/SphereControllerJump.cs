using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereController))]
public class SphereControllerJump : MonoBehaviour {

	//adapted from http://answers.unity3d.com/questions/334708/gravity-with-character-controller.html

	KeyCode JumpKey = KeyCode.Space;

	public float Gravity = 9.8f;

	public float JumpSpeed = 8;
	public float VerticalSpeed = 0;

	SphereController Sphere;

	bool isJumping = false;

	public GameObject Particles;

	bool inputPressed = false;
	bool inputReleased = false;

	public float minSpeedParticles = -1.0f;
	public float maxSpeedParticles = -1.8f;
	
	// Use this for initialization
	void Start () {
		Sphere = GetComponent<SphereController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKeyDown(JumpKey))
			InputPressed();
		if (Input.GetKeyUp(JumpKey))
			InputReleased();
		CheckSphereJump();
	}

	void CheckSphereJump() {
		if (!isJumping && Sphere.Controller.isGrounded)
		{
			if (inputPressed)
			{
				isJumping = true;
				VerticalSpeed = JumpSpeed;
			}
		}

		if (inputReleased && isJumping)
		{
			if (VerticalSpeed > 0)
				VerticalSpeed = 0;
			isJumping = false;
		}

		if (Sphere.Controller.isGrounded && !isJumping)
		{
			if (inputPressed){ // unless it jumps:
				isJumping = true;
				VerticalSpeed = JumpSpeed;
			}
			else
			{
				VerticalSpeed = 0; // grounded character has vSpeed = 0...
			}
		}
		// apply gravity acceleration to vertical speed:
		VerticalSpeed -= Gravity * Time.deltaTime;
		Sphere.SphereMovementLocal.y = VerticalSpeed; // include vertical speed in vel

		// convert vel to displacement and Move the character:
		//controller.Move(vel * Time.deltaTime);
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		//ignore hits below us, as the character controller handles these

		if (VerticalSpeed < minSpeedParticles)
		{
			float particleLifetime = VerticalSpeed / maxSpeedParticles;
			float particleDensity = (VerticalSpeed - minSpeedParticles) / (maxSpeedParticles - minSpeedParticles); 
			if (Particles != null)
			{
				Particles.particleSystem.maxParticles = (int)(particleDensity * 1000);
				Particles.particleSystem.startLifetime = particleLifetime * 0.3f;
				Particles.particleSystem.Play();
			}

			//Debug.Log (VerticalSpeed);
		}
		VerticalSpeed = 0;
	}

	public void InputPressed()
	{
		inputReleased = false;
		inputPressed = true;
	}

	public void InputReleased()
	{
		inputPressed = false;
		inputReleased = true;
	}



}
