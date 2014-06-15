using UnityEngine;
using System.Collections;

public class CollisionData : MonoBehaviour {

	public SoundController.type collisionSound;
	public SoundController.type dragSound;

	//TODO(Rob): Add your movement modifiers here in a similar fashion

	public SoundController.type GetCollisionSound() {
		return collisionSound;
	}

	public SoundController.type GetDragSound() {
		return dragSound;
	}
}
