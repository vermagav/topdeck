using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class PlayerMovement : MonoBehaviour {

	Vector3 targetVelocity = new Vector3();
	
	public float maxVelocity;
	public float maxForce;

	public float movementRate = 1;

	private bool grounded = false;

	private int currentCollisionBodyID = -1;
	private CollisionData collisionData;

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
			targetVelocity *= maxVelocity * movementRate;
			
			Vector3 force = Vector3.ClampMagnitude((targetVelocity - rigidbody.velocity)/rigidbody.mass, maxForce * movementRate)/Time.deltaTime;

			rigidbody.AddForce(force);
		}

		grounded = false;
	}

	void OnCollisionEnter(Collision collision) {
		if (currentCollisionBodyID != collision.gameObject.GetInstanceID())
		{
			currentCollisionBodyID = collision.gameObject.GetInstanceID();
			//this is a new colliding object, fetch fresh collision data only on change
			CollisionData cData = collision.gameObject.GetComponent<CollisionData>();
			collisionData = cData;
		}

		if (collisionData == null)
		{
			return;
		}

		//TODO: apply custom force using the CollisionData object (maybe)

	}
	
	void OnCollisionStay (Collision collision) {
		grounded = true;  

		if (currentCollisionBodyID != collision.gameObject.GetInstanceID())
		{
			currentCollisionBodyID = collision.gameObject.GetInstanceID();
			//this is a new colliding object, fetch fresh collision data only on change
			CollisionData cData = collision.gameObject.GetComponent<CollisionData>();
			collisionData = cData;
		}

		if (collisionData == null)
		{
			//TODO: restore the movement rate to some default
			movementRate = 1;
			return;
		}

		movementRate = Surface.GetMovementRate(collisionData.GetSubstance());
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