using UnityEngine;
using System.Collections;

public class InterestLevel : MonoBehaviour {

	public float levelOfInterest;

	SphereCollider interestRangeCollider;
	int defaultInterestRadius = 10;

	void Start() 
	{
		SphereCollider[] spheres = GetComponents<SphereCollider>();
		foreach (SphereCollider sphere in spheres)
		{
			if (sphere.isTrigger)
			{
				if (interestRangeCollider == null)
					interestRangeCollider = sphere;
				else
					Debug.LogError ("Only one sphere collider attached to this object of interest should be a trigger.");
			}
		}
		if (interestRangeCollider == null)
		{
			//we did not find a collider with a trigger, so we will add one now.
			interestRangeCollider = gameObject.AddComponent<SphereCollider>();
			interestRangeCollider.isTrigger = true;
			interestRangeCollider.radius = defaultInterestRadius * 	(1 / transform.localScale.x); 
																	//in case the parent object has been scaled down
		}
	}
}
