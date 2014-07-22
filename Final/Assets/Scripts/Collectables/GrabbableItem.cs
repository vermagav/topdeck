using UnityEngine;
using System.Collections;

public class GrabbableItem : MonoBehaviour {

	ObjectHighlights feedbackHighlights;
	bool feedbackPresent { get { return feedbackHighlights != null; } }

	//experiment with different types of joints here
	FixedJoint joint = null;

	// Use this for initialization
	void Start () {
		gameObject.tag = "Collectable";
		feedbackHighlights = GetComponent<ObjectHighlights>();
		/*
		if (!feedbackPresent)
			feedbackHighlights = gameObject.AddComponent<ObjectHighlights>();
		*/
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
		if (feedbackPresent)
			feedbackHighlights.SetHighlight(true, 0);
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
		if (feedbackPresent)
			feedbackHighlights.SetHighlight(false, 0);
	}
}