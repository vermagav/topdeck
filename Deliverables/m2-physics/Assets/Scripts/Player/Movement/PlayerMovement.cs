using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class PlayerMovement : MonoBehaviour {

	Vector3 targetVelocity = new Vector3();
	
	public float maxVelocity;
	public float maxForce;

	private bool grounded = false;

	void Awake () {
		rigidbody.freezeRotation = true;
	}
	
	void FixedUpdate () {

		if (grounded) {

			if(targetVelocity.magnitude > 1)
			{
				targetVelocity = targetVelocity / targetVelocity.magnitude;
			}

			targetVelocity = transform.TransformDirection(targetVelocity);
			targetVelocity *= maxVelocity;
			
			Vector3 force = Vector3.ClampMagnitude((targetVelocity - rigidbody.velocity)/rigidbody.mass, maxForce)/Time.deltaTime;

			rigidbody.AddForce(force);
		}

		grounded = false;
	}
	
	void OnCollisionStay () {
		grounded = true;    
	}

	public void SetTargetVelocity(Vector3 velocity)
	{
		targetVelocity = velocity;
	}

	public Vector3 GetTargetVelocity() 
	{
		return targetVelocity;
	}

}