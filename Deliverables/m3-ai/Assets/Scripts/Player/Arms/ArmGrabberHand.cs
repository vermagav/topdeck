using UnityEngine;
using System.Collections;

public class ArmGrabberHand : MonoBehaviour {

	ArmGrabber parentArm;
	SphereCollider grabRange;
	public GameObject grabPoint;

	public bool isOpen = false;

	public GrabbableItem currentCollectableInRange;
	public GrabbableItem itemHeld;
	Vector3 connectionPoint;

	// Use this for initialization
	void Start () {
		grabRange = GetComponent<SphereCollider>();
		grabRange.isTrigger = true;
		parentArm = transform.parent.GetComponent<ArmGrabber>();
		//grabSpring = grabPoint.GetComponent<SpringJoint>();
		//grabPoint.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay (Collider col) {
		if (col.gameObject.CompareTag("Collectable"))
		{
			GrabbableItem item = col.GetComponent<GrabbableItem>();
			if (item != currentCollectableInRange && item != null)
			{
				currentCollectableInRange = item;
			}
		}
	}

	void OnTriggerExit (Collider col) {
		if (col.gameObject.CompareTag("Collectable"))
		{
			//Debug.LogError ("Trigger Exit");
			GrabbableItem item = col.GetComponent<GrabbableItem>();
			if (item == currentCollectableInRange)
			{
				currentCollectableInRange = null;
			}
		}
	}

	public void SetHandClosed (float axis)
	{
		if (axis < 0.8) //open hand
		{
			isOpen = true;
			if (itemHeld != null)
			{
				itemHeld.ReleaseItem();
			}
			grabPoint.SetActive (false);
		}
		else
		{
			isOpen = false;
			if (currentCollectableInRange != null)
			{
				grabPoint.SetActive(true);

				itemHeld = currentCollectableInRange;
				itemHeld.rigidbody.isKinematic = true;
				itemHeld.transform.position = grabPoint.transform.position;
				itemHeld.GrabItem(grabPoint.rigidbody);
				itemHeld.rigidbody.isKinematic = false;
			}
		}
	}
}
