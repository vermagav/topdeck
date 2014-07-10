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

			//targetVelocity = transform.TransformDirection(targetVelocity);
			if((targetVelocity != rigidbody.velocity))
			{
				targetVelocity *= maxVelocity * movementRate;
				Vector3 force = Vector3.zero;
				force = Vector3.Scale((targetVelocity - rigidbody.velocity), XZplane);
				force = force / force.magnitude * maxForce * movementRate;

				force *= Vector3.Dot(groundNormal, Vector3.up);

				force =  Quaternion.AngleAxis(90f - Vector3.Angle(force, groundNormal), Vector3.Cross(groundNormal, force)) * force;
				Debug.Log (force);
				rigidbody.AddForce(force);
			}
		}



		groundNormal = Vector3.zero;
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

}