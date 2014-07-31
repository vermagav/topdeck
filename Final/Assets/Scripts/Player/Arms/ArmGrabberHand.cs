using UnityEngine;
using System.Collections;

public class ArmGrabberHand : BaseRobotHand {

	public float axisOpeningThreshold = 0.2f;
	public float axisClosingThreshold = 0.7f;
	
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
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (itemHeld != null)
		{
			//HACK: Such a hack
			itemHeld.RotateItemWithBody(GetTorsoRotationState());
		}
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

	public override void UpdateInputAxis (float axis)
	{
		if (axis < axisOpeningThreshold) //hand is opened
		{
			isOpen = true;
			if (itemHeld != null)
			{
				itemHeld.ReleaseItem();
				itemHeld = null;
			}
			grabPoint.SetActive (false);
		}
		else if (axis > axisClosingThreshold) //hand is closed
		{
			isOpen = false;
			if (currentCollectableInRange != null)
			{
				grabPoint.SetActive(true);

				itemHeld = currentCollectableInRange;
				itemHeld.rigidbody.isKinematic = true;

				//HACK: Keeps the held object from flying away (should drop straight down)
				itemHeld.transform.position = grabPoint.transform.position + transform.forward * (itemHeld.transform.localScale.x - 1);

				itemHeld.GrabItem(grabPoint.rigidbody);
				itemHeld.rigidbody.isKinematic = false;
			}
		}
	}
}
