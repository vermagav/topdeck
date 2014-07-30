using UnityEngine;
using System.Collections;

public class GameLoader : MonoBehaviour {

	public KeyCode resetKey = KeyCode.Escape;
	public string levelName = "main";

	// Use this for initialization
	void Start () {
		gameObject.name = "GameLoader";
		DontDestroyOnLoad(this);
		LoadMainLevel();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(resetKey))
		{
			LoadMainLevel();
		}
	}

	void LoadMainLevel()
	{
		Application.LoadLevel(levelName);
	}


}
