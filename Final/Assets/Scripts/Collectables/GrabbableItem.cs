using UnityEngine;
using System.Collections;

public class GrabbableItem : MonoBehaviour {

	float timeToFreezeRotationOnGrab = 1f;
	Vector3 rotationOnGrab;

	ObjectHighlights feedbackHighlights;
	bool feedbackPresent { get { return feedbackHighlights != null; } }

	//experiment with different types of joints here
	FixedJoint joint = null;

	public bool movementDampened = false;

	// Use this for initialization
	void Start () {
		//joint = gameObject.AddComponent<FixedJoint>();
		gameObject.tag = "Collectable";
		gameObject.layer = LayerMask.NameToLayer("Default");
		feedbackHighlights = GetComponent<ObjectHighlights>();

		if (!feedbackPresent)
			feedbackHighlights = gameObject.AddComponent<ObjectHighlights>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (joint != null)
		{
			if (movementDampened && rigidbody.angularVelocity.magnitude < 0.1f)
			{
				rigidbody.angularVelocity = new Vector3(0, 0, 0);
				movementDampened = false;
			}

			//Debug.DrawRay(joint.transform.position, joint.connectedBody.transform.position, Color.green, (1/30f), false);
		}
	
	}

	public void GrabItem(Rigidbody grabPoint)
	{
		//TODO: Objects should collide with player instead of passing through
		//gameObject.layer = LayerMask.NameToLayer("Player");
		gameObject.layer = LayerMask.NameToLayer("Interactable");
		rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
		//rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		rotationOnGrab = transform.localEulerAngles;
		FreezeRotation();
		joint = gameObject.AddComponent<FixedJoint>();
		joint.connectedBody = grabPoint;
		if (feedbackPresent)
		{
			feedbackHighlights.RestoreHighlightMaterial();
			feedbackHighlights.SetHighlight(true, 0);
			feedbackHighlights.highlightPulse = true;
		}
		//rigidbody.isKinematic = true;

	}

	public void ReleaseItem()
	{
		gameObject.layer = LayerMask.NameToLayer("Default");
		rigidbody.interpolation = RigidbodyInterpolation.None;
		//rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
		if (joint != null)
		{
			//TODO: Make this work in conjunction with analog stick travel
			joint.connectedBody = null;
			if (IsInvoking ("FreezeRotation"))
			{
				CancelInvoke("FreezeRotation");
			}
			Destroy (joint);
		}

		rigidbody.constraints = RigidbodyConstraints.None;
		if (feedbackPresent)
		{
			feedbackHighlights.RestoreOriginalMaterial();
			feedbackHighlights.SetHighlight(false, 0);
			feedbackHighlights.highlightPulse = false;
		}
	}

	public void RotateItemWithBody(Quaternion rotation)
	{
		//if we aren't rotating, we don't need to add any more torque
		if (rotation.eulerAngles.magnitude < 1)
			return;

		//unlock y rotation for the object and spin it in the direction fo player body movement
		AllowRotationY();
		rigidbody.AddTorque (rotation.eulerAngles * (1 / rigidbody.mass));
		//if we have a countdown to freeze rotation, cancel it
		if (IsInvoking ("FreezeRotation"))
		{
			CancelInvoke("FreezeRotation");
		}
		//reset the rotation freeze timer
		Invoke ("FreezeRotation", timeToFreezeRotationOnGrab);
		//transform.localEulerAngles = rotation.eulerAngles + rotationOnGrab;
		//rigidbody.isKinematic = false;
	}

	public void MoveItemWithBody (Vector3 velocity)
	{
		rigidbody.AddForce(velocity);
	}


	public void FreezeRotation()
	{
		//rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
		//rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
		movementDampened = true;
	}

	public void AllowRotationY()
	{
		//rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		//rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		movementDampened = false;
	}

}