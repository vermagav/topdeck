using UnityEngine;
using System.Collections;

public class GroundTrigger : MonoBehaviour {

	public Texture buttonOff;
	public Texture buttonOn;

	public AnimateDoor animateDoor;

	bool triggerActive = false;

	void OnCollisionStay () {
		// Change texture
		renderer.material.mainTexture = buttonOn;

		// Lower the fence
		animateDoor.OpenDoor ();

		triggerActive = true;
	}

	void OnCollisionExit () {
		// Change texture
		renderer.material.mainTexture = buttonOff;

		// Raise the fence ヽ༼ຈلຈ༽ﾉ
		animateDoor.CloseDoor ();

		triggerActive = false;
	}

	public bool GetTriggerState() 
	{
		return triggerActive;
	}
}
