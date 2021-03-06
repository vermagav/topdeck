﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SubtitleDisplay : MonoBehaviour {

	public bool useALLCAPS = false;
	static Color BlankColor = new Color(0, 0, 0, 0);
	public GUITexture backgroundTexture;

	float lastSubtitleBegin;
	float lastSubtitleEnd;
	bool subtitlesDisplayed = false;
	Color defaultTextColor;
	Color subtitleTextColor;
	Color backgroundTextureColor;
	float fadeSpeed = 0.2f;
	float fadeStatus = 0;
	List<Subtitle> subtitles;

	// Use this for initialization
	void Awake () {
		subtitles = new List<Subtitle>();
		ClearSubtitles();
		defaultTextColor = guiText.color;
		backgroundTextureColor = backgroundTexture.color;
		subtitleTextColor = defaultTextColor;
	}

	void Start() {
		//Test ();
	}
	
	// Update is called once per frame
	void Update () {
		if (subtitlesDisplayed && Time.timeSinceLevelLoad > lastSubtitleEnd)
		{
			subtitlesDisplayed = false;
		}
		if (subtitlesDisplayed)
		{
			//fade subtitles in
			fadeStatus = (Time.timeSinceLevelLoad - lastSubtitleBegin) / fadeSpeed;
			guiText.color = Color.Lerp(Color.clear, subtitleTextColor, fadeStatus);
			backgroundTexture.color = Color.Lerp(Color.clear, backgroundTextureColor, fadeStatus);
		}
		else
		{
			//fade subtitles out
			fadeStatus = (Time.timeSinceLevelLoad - lastSubtitleEnd) / fadeSpeed;
			guiText.color = Color.Lerp(subtitleTextColor, Color.clear, fadeStatus);
			backgroundTexture.color = Color.Lerp(backgroundTextureColor, Color.clear, fadeStatus);
			if (fadeStatus > 1)
			{
				AdvanceSubtitles();
			}
		}
	}

	public void StartSubtitles()
	{
		if (subtitles.Count != 0 && !subtitlesDisplayed)
			UpdateDisplay(subtitles[0]);
	}

	public void AddSubtitle(string text, float length)
	{
		AddSubtitle (text, length, BlankColor);
	}
	
	//http://answers.unity3d.com/questions/45007/c-optional-parameters.html
	//does not work with colors, apparently

	public void AddSubtitle(string text, float length, Color color)
	{
		EnqueueSubtitle (new Subtitle(text, length, color));
	}

	public void AdvanceSubtitles()
	{
		if (subtitles.Count == 0)
			return;
		subtitles.RemoveAt(0);
		if (subtitles.Count == 0)
			return;
		UpdateDisplay (subtitles[0]);
	}

	void EnqueueSubtitle(Subtitle sub)
	{
		subtitles.Add(sub);
	}

	void UpdateDisplay(Subtitle sub)
	{
		string text = sub.text;
		if (useALLCAPS)
			text = text.ToUpper();
		if (sub.color == BlankColor)
			subtitleTextColor = defaultTextColor;
		else
			subtitleTextColor = sub.color;
		gameObject.guiText.text = text;

		//http://answers.unity3d.com/questions/27818/positioning-guilabel-behind-guitext.html
		Rect r =  guiText.GetScreenRect();
		Rect textureRect = new Rect(0, -Screen.height / 2 + guiText.fontSize, Screen.width, guiText.fontSize);
		backgroundTexture.pixelInset = textureRect;
		//guiTexture.pixelInset.width = r.width;
		//guiTexture.pixelInset.height = r.height;
		//guiTexture.pixelInset.y = -r.height;

		lastSubtitleBegin = Time.timeSinceLevelLoad;
		lastSubtitleEnd = lastSubtitleBegin + sub.length;
		subtitlesDisplayed = true;
	}


	public void ClearSubtitles()
	{
		subtitlesDisplayed = false;
		subtitles.Clear();
	}

	void Test()
	{
		SubtitleManager.Instance.AddSubtitle ("OH MY GOD...", 1f);
		SubtitleManager.Instance.AddSubtitle ("A BOX.", 1f);
		SubtitleManager.Instance.AddSubtitle ("<3 <3 <3 <3 <3", 2f, Color.magenta);
	}
}
