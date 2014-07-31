using UnityEngine;
using System.Collections;

public class GameLoaderSimple : MonoBehaviour {

	public KeyCode resetKey = KeyCode.Escape;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(resetKey))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
