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
		if (Sphere.Controller.isGrounded)
		{
			isJumping = false;
			if (inputPressed)
			{
				VerticalSpeed = JumpSpeed;
			}
			else
			{
				VerticalSpeed = 0;
			}
		}
		if (isJumping)
		{
			if (!inputPressed)
				VerticalSpeed = 0;
			if (VerticalSpeed <= 0)
				isJumping = false;
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
		inputPressed = true;
	}

	public void InputReleased()
	{
		inputPressed = false;
	}



}
