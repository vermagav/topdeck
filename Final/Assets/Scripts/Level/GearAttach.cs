using UnityEngine;
using System.Collections;

public class GearAttach : MonoBehaviour {

	public GameObject targetGear;

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.Equals(targetGear))
		{
			GetComponent<Animator>().SetBool("hasCenterGear", true);
			other.gameObject.SetActive(false);
		}
	}
}
