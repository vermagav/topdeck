using UnityEngine;
using System.Collections;

public class GrabbableItem : MonoBehaviour {

	//experiment with different types of joints here
	FixedJoint joint = null;

	// Use this for initialization
	void Start () {
		gameObject.tag = "Collectable";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GrabItem(Rigidbody grabPoint)
	{
		//TODO: Objects should collide with player instead of passing through

		gameObject.layer = LayerMask.NameToLayer("Player");
		rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
		joint = gameObject.AddComponent<FixedJoint>();
		joint.connectedBody = grabPoint;
		//rigidbody.isKinematic = true;

	}

	public void ReleaseItem()
	{
		gameObject.layer = LayerMask.NameToLayer("Default");
		if (joint != null)
		{
			//TODO: Make this work in conjunction with analog stick travel
			Destroy (joint);
		}

		rigidbody.constraints = RigidbodyConstraints.None;
	}
}