using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class PlayerMovement : MonoBehaviour {
	
	public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	//public bool canJump = true;
	//public float jumpHeight = 2.0f;
	private bool grounded = false;

	void Awake () {
		rigidbody.freezeRotation = true;
		rigidbody.useGravity = false;
	}
	
	void FixedUpdate () {

		if (grounded) {
			Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			if(targetVelocity.magnitude > 1)
			{
				targetVelocity = targetVelocity / targetVelocity.magnitude;
			}

			targetVelocity = transform.TransformDirection(targetVelocity);
			targetVelocity *= speed;
			
			Vector3 velocity = rigidbody.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
			
			/*// Jump
			if (canJump && Input.GetButton("Jump")) {
				rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			}*/
		}
		
		rigidbody.AddForce(new Vector3 (0, -gravity * rigidbody.mass, 0));
		
		grounded = false;
	}
	
	void OnCollisionStay () {
		grounded = true;    
	}
	
	/*float CalculateJumpVerticalSpeed () {
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}*/
}