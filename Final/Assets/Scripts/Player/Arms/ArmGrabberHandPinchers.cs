using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ArmGrabberHand))]
public class ArmGrabberHandPinchers : MonoBehaviour {

	ArmGrabberHand hand;
	public GameObject leftPinchers;
	public GameObject rightPinchers;
	SphereCollider rigidBodyCollider;

	// Use this for initialization
	void Start () {
		hand = GetComponent<ArmGrabberHand>();
		rigidBodyCollider = transform.FindChild("GrabPoint").GetComponent<SphereCollider>();
	
	}
	
	// Update is called once per frame
	void Update () {
		SetPinchers(hand.isOpen);

	}

	void SetPinchers(bool state)
	{
		if (state)
		{
			//open pinchers
			leftPinchers.transform.localEulerAngles = new Vector3(0, 45, 0);
			rightPinchers.transform.localEulerAngles = new Vector3(0, -45, 180);
			rigidBodyCollider.enabled = false;
		}
		else
		{
			//close pinchers
			leftPinchers.transform.localEulerAngles = new Vector3(0, 0, 0);
			rightPinchers.transform.localEulerAngles = new Vector3(0, 0, 180);
			rigidBodyCollider.enabled = true;
		}
	}




}
