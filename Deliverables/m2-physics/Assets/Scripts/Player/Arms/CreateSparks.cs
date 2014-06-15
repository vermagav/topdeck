using UnityEngine;
using System.Collections;

public class CreateSparks : MonoBehaviour {

	public GameObject Sparks;
	private GameObject sparksInstantiation;

	void FixedUpdate()
	{
		if(Input.GetButtonDown("Fire2"))
		{
			Destroy(sparksInstantiation);

			sparksInstantiation = (GameObject)Instantiate(Sparks, transform.position, transform.rotation);
			sparksInstantiation.transform.parent = transform;
		}

		if(!Input.GetButton("Fire2"))
		{
			Destroy(sparksInstantiation);
		}
	}
}
