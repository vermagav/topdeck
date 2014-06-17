using UnityEngine;
using System.Collections;

public class CollisionData : MonoBehaviour {

	public Surface.Substance substance;
	
	//TODO(Rob): Add your movement modifiers here in a similar fashion
	
	public Surface.Substance GetSubstance() {
		return substance;
	}

	void OnCollisionEnter(Collision collision) {

		//TODO: This could be shaped infinitely better, this is a very crude operation responding to velocity currently

		// Play the appropriate sound at the point of contact
		float colMagnitude = Mathf.Log10(collision.relativeVelocity.magnitude);

		float thresholdToPlay = 0.2f;

		if (colMagnitude > thresholdToPlay)
		{
			Debug.Log (collision.gameObject.name + " - Magnitude: " + colMagnitude);
			SoundController.Instance.Play (GetSubstance(), Surface.Cue.Collide, this.transform.position, colMagnitude);
		}
	}
}
