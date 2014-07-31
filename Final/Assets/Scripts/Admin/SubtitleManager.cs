using UnityEngine;
using System.Collections;

public class SubtitleManager : MonoBehaviour {

	public SubtitleDisplay display;

	static SubtitleManager _instance;

	public static SubtitleManager Instance
	{
		get
		{
			if (_instance == null)
			{
				Debug.Log ("SubtitleManager not found. Instantiating from Resources directory.");
				GameObject go = Instantiate(Resources.Load ("Prefabs/SubtitleManager")) as GameObject;
				GameObject systems = GameObject.Find ("Game Systems");
				go.transform.parent = systems.transform;
				_instance = go.GetComponent<SubtitleManager>();
			}

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

	public void Play () {
		if (display != null)
		{
			display.StartSubtitles();
		}
	}

	public void Stop () {
		if (display != null)
		{
			display.ClearSubtitles();
		}
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
