using UnityEngine;
using System.Collections;

public class SubtitleManager : MonoBehaviour {

	public SubtitleDisplay display;

	static SubtitleManager _instance;

	public static SubtitleManager Instance
	{
		get
		{
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
		display.AddSubtitle(text, length);
	}

	public void AddSubtitle(string text, float length, Color color)
	{
		display.AddSubtitle(text, length, color);
	}
}
