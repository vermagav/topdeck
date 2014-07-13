using UnityEngine;
using System.Collections;
using InControl;

public class GameController : MonoBehaviour {
	

	void Awake()
	{
		Screen.showCursor = false;
		InputManager.Setup ();
	}

	void Update()
	{
		InputManager.Update ();
	}
}
