using UnityEngine;
using System.Collections;

public class GroundTrigger : MonoBehaviour {

	public Texture buttonOff;
	public Texture buttonOn;

	public FenceController fenceController;

	bool triggerActive = false;

	void OnCollisionStay () {
		// Change texture
		renderer.material.mainTexture = buttonOn;

		// Lower the fence
		fenceController.setFenceUp (false);

		triggerActive = true;
	}

	void OnCollisionExit () {
		// Change texture
		renderer.material.mainTexture = buttonOff;

		// Raise the fence ヽ༼ຈلຈ༽ﾉ
		fenceController.setFenceUp (true);

		triggerActive = false;
	}

	public bool GetTriggerState() 
	{
		return triggerActive;
	}
}
