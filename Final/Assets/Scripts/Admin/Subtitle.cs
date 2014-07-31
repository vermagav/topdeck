using UnityEngine;
using System.Collections;

[System.Serializable]
public class Subtitle {

	public string text;
	public float length;
	public Color color;

	public Subtitle(string t, float l, Color c)
	{
		text = t;
		length = l;
		color = c;
	}
}
