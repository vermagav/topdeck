using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	Vector3 targetVelocity = new Vector3();

	public float maxVelocity;
	public float maxForce;

	public float movementRate = 1;
	private Vector3 groundNormal;

	private int currentCollisionBodyID = -1;
	private CollisionData collisionData;
	private Vector3 XZplane;

	private Vector3 forceApplied;

	void Awake()
	{
		groundNormal = Vector3.zero;
		XZplane = new Vector3(1, 0, 1);
	}

	void FixedUpdate () {

		if (!groundNormal.Equals(Vector3.zero)) {

			if(targetVelocity.magnitude > 1)
			{
				targetVelocity = targetVelocity / targetVelocity.magnitude;
			}
			targetVelocity *= maxVelocity * movementRate;

			//targetVelocity = transform.TransformDirection(targetVelocity);

			forceApplied = Vector3.zero;

			if(rigidbody != null && (targetVelocity - rigidbody.velocity).magnitude > 1f)
			{
				forceApplied = Vector3.Scale((targetVelocity - rigidbody.velocity), XZplane);

				if(forceApplied.magnitude < 0.1f)
					return;

				forceApplied = forceApplied / forceApplied.magnitude * maxForce * movementRate;

				forceApplied *= Vector3.Dot(groundNormal, Vector3.up);

				forceApplied =  Quaternion.AngleAxis(90f - Vector3.Angle(forceApplied, groundNormal), Vector3.Cross(groundNormal, forceApplied)) * forceApplied;

				rigidbody.AddForce(forceApplied);
			}
		}



		groundNormal = Vector3.zero;
	}

	void UpdateVelocity(Vector3 movementDirection)
	{
		SetTargetVelocity(movementDirection);
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
		ContactPoint poc = collision.contacts[0];
		for(int i = 1; i < collision.contacts.Length; i++)
		{
			if(collision.contacts[i].point.y < poc.point.y)
			{
				poc = collision.contacts[i];
			}
		}

		if(groundNormal.Equals(Vector3.zero) || Vector3.Dot(groundNormal, Vector3.up) < Vector3.Dot (poc.normal, Vector3.up))
		{
			groundNormal = poc.normal;
		}



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



		/*Vector3 tempGroundNormal = Vector3.up;
		float tempGroundDotUp = 0f;

		for(int i = 0; i < collision.contacts.Length; i++)
		{
			float possibleGroundDotUp = Vector3.Dot (collision.contacts[i].normal, Vector3.up);
			if(possibleGroundDotUp > tempGroundDotUp)
			{
				tempGroundDotUp = possibleGroundDotUp;
				tempGroundNormal = collision.contacts[i].normal;
			}
		}

		if(groundNormal.Equals(Vector3.zero) || Vector3.Dot(groundNormal, Vector3.up) < Vector3.Dot (tempGroundNormal, Vector3.up))
		{
			groundNormal = tempGroundNormal;
		}*/
	}

	public void SetTargetVelocity(Vector3 velocity)
	{
		targetVelocity = velocity;
	}

	public Vector3 GetTargetVelocity() 
	{
		return targetVelocity;
	}

	public Vector3 GetMovementForce()
	{
		return forceApplied;
	}

}