using UnityEngine;
using System.Collections;

public class PlayerArmSocket : MonoBehaviour {
	
	public GameObject acquiredBuff;
	public GameObject connector;

	public SwayArm currentArm;

	void OnTriggerEnter (Collider other)
	{
		//Debug.Log ("Pickup Arm Collided");
		if(other.gameObject.CompareTag("Arm"))
		{
			CollectableHighlights collectable = other.gameObject.GetComponent<CollectableHighlights>();
			if (collectable != null)
			{
				collectable.RemoveHighlights();
				other.gameObject.transform.position = connector.transform.position;
				other.gameObject.transform.rotation = connector.transform.rotation;
				other.gameObject.transform.parent = connector.transform;
				currentArm = other.gameObject.GetComponent<SwayArm>();
			}
			else //legacy code for old prefabs
			{
				Destroy(other.gameObject);
				GameObject newArm = (GameObject)Instantiate(acquiredBuff, connector.transform.position, connector.transform.rotation);
				newArm.transform.parent = connector.transform;
				//TODO: Check what kind of arm it is, we know it is a sway arm currently
				currentArm = newArm.GetComponent<SwayArm>();
			}
		}
	}

	public void SetArmState(bool state)
	{
		if (currentArm != null)
			currentArm.SendMessage("SetArmState", state);
	}
}
