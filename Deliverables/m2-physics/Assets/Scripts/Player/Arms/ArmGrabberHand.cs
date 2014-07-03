using UnityEngine;
using System.Collections;

public class ArmGrabberHand : MonoBehaviour {

	ArmGrabber parentArm;
	SphereCollider grabRange;
	public GameObject grabPoint;
	SpringJoint grabSpring;

	public bool isOpen = false;

	Collider currentCollectableInRange;
	Vector3 connectionPoint;

	// Use this for initialization
	void Start () {
		grabRange = GetComponent<SphereCollider>();
		grabRange.isTrigger = true;
		parentArm = transform.parent.GetComponent<ArmGrabber>();
		grabSpring = grabPoint.GetComponent<SpringJoint>();
		grabPoint.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay (Collider col) {
		if (col.gameObject.CompareTag("Collectable"))
		{
			if (col != currentCollectableInRange)
			{
				currentCollectableInRange = col;
			}
		}
	}

	void OnTriggerExit (Collider col) {
		if (col.gameObject.CompareTag("Collectable"))
		{
			//Debug.LogError ("Trigger Exit");
			if (col == currentCollectableInRange)
			{
				currentCollectableInRange = null;
			}
		}
	}

	public void SetHandClosed (bool status)
	{
		if (!status) //open hand
		{
			isOpen = true;
			if (grabSpring.connectedBody != null)
			{
				grabSpring.connectedBody = null;
			}
			grabPoint.SetActive (false);
		}
		else
		{
			isOpen = false;
			if (currentCollectableInRange != null)
			{
				grabPoint.SetActive(true);
				grabSpring = grabPoint.GetComponent<SpringJoint>();
				grabSpring.connectedBody = currentCollectableInRange.rigidbody;
			}
		}
	}
}
