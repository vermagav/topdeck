using UnityEngine;
using System.Collections;

public class ComputerScreen : MonoBehaviour {

	void Start() {
		renderer.enabled = false;
	}

	public void TurnOnScreen() {
		renderer.enabled = true;
	}
}
