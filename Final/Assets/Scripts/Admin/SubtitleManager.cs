using UnityEngine;
using System.Collections;

public class SubtitleManager : MonoBehaviour {

	public SubtitleDisplay display;

	static SubtitleManager _instance;

	public static SubtitleManager Instance
	{
		get
		{
			/*
			if (_instance == null)
			{
				GameObject go = new GameObject("SubtitleManager");
				GameObject displayGO = new GameObject("Subtitles");
				displayGO.AddComponent<SubtitleDisplay>();
				displayGO.transform.localPosition = new Vector3(0.5f, 0.02f, 0);
				displayGO.transform.parent = go.transform;
				_instance = go.AddComponent<SubtitleManager>();
			}
			*/
			return _instance;
		}
	}

	// Use this for initialization
	void Awake () {
		gameObject.name = "SubtitleManager";
		if (_instance == null)
			_instance = this;
		if (display == null)
			display = FindObjectOfType<SubtitleDisplay>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddSubtitle(string text, float length)
	{
		if (display != null)
			display.AddSubtitle(text, length);
	}

	public void AddSubtitle(string text, float length, Color color)
	{
		if (display != null)
			display.AddSubtitle(text, length, color);
	}
}
