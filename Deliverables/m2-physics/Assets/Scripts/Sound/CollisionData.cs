using UnityEngine;
using System.Collections;

public class CollisionData : MonoBehaviour {

	public Surface.Substance substance;
	
	//TODO(Rob): Add your movement modifiers here in a similar fashion
	
	public Surface.Substance GetSubstance() {
		return substance;
	}
}
