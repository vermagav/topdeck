using UnityEngine;
using System.Collections;

public class CreateSparks : MonoBehaviour {

	public GameObject Sparks;
	private GameObject sparksInstantiation;

	public void PlaySparks() //GetButtonDown
	{
		Destroy(sparksInstantiation);
		
		sparksInstantiation = (GameObject)Instantiate(Sparks, transform.position, transform.rotation);
		sparksInstantiation.transform.parent = transform;
	}

	public void KillSparks()
	{
		Destroy(sparksInstantiation);
	}
}
