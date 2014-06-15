using UnityEngine;
using System.Collections;

public class PickupArm : MonoBehaviour {

	public GameObject target;
	public GameObject acquiredBuff;
	public GameObject connector;

	public SwayArm currentArm;

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject == target)
		{
			Destroy(other.gameObject);
			GameObject newArm = (GameObject)Instantiate(acquiredBuff, connector.transform.position, connector.transform.rotation);
			newArm.transform.parent = connector.transform;
			//TODO: Check what kind of arm it is, we know it is a sway arm currently
			currentArm = newArm.GetComponent<SwayArm>();
		}
	}

	public void SetArmState(bool state)
	{
		if (currentArm != null)
			currentArm.SendMessage("SetArmState", state);
	}
}
