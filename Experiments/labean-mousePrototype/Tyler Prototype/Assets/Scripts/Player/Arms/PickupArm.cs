using UnityEngine;
using System.Collections;

public class PickupArm : MonoBehaviour {

	public GameObject target;
	public GameObject acquiredBuff;
	public GameObject connector;

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject == target)
		{
			Destroy(other.gameObject);
			GameObject newArm = (GameObject)Instantiate(acquiredBuff, connector.transform.position, connector.transform.rotation);
			newArm.transform.parent = connector.transform;
		}
	}
}
