using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(SoundController))] //this tells unity which class we intend to overwrite it's Inspector behavior for
public class SoundControllerEditor : Editor {

	//interesting:
	//http://blog.brendanvance.com/2014/04/08/elegant-editor-only-script-execution-in-unity3d/

	private SoundController sc;

	// Styles for different aspects of the GUI
	GUIStyle defaultStyle;
	GUIStyle headlineStyle;
	GUIStyle sectionStyle;
	
	public void OnEnable()
	{
		//this lets us reference the sound controller directly with the shortcut
		sc = (SoundController)target;
		if (sc.surfaceData == null)
			sc.RegenerateSurfaceProperties();
		CreateStyles();
	}



	public override void OnInspectorGUI()
	{
		foreach (SurfaceSoundData data in sc.surfaceData)
		{
			//headline for each data type
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(data.substance.ToString().ToUpper(), headlineStyle);
			EditorGUILayout.EndHorizontal();

			//SFX
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("SFX", sectionStyle);
			EditorGUILayout.EndHorizontal();

			//Dragging Sound
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Drag:", GUILayout.Width (80));
			data.sfxDraggingSound = EditorGUILayout.ObjectField(data.sfxDraggingSound, typeof(AudioClip), true, GUILayout.Width(180)) as AudioClip;
			EditorGUILayout.EndHorizontal();

			//Collision Sound(s)
			GUILayout.BeginHorizontal(); //begin line [layouts "begin" and "end"]

			GUILayout.Label ("Collision:", GUILayout.Width (80)); //header
			if (GUILayout.Button("-", GUILayout.Width (20))) //button to remove clips dynamically (from end)
			{
				if (data.sfxCollisionBank.Count > 0)
					data.sfxCollisionBank.RemoveAt (data.sfxCollisionBank.Count - 1);
			}
			if (GUILayout.Button("+", GUILayout.Width (20))) //button to add new clips dynamically
			{
				data.sfxCollisionBank.Add (null);
			}

			if (data.sfxCollisionBank.Count == 0) //report if we have no clips currently
			{
				GUILayout.Label ("No clips present.", GUILayout.Width (100));
			}
			else //show the number of clips we have
			{
				GUILayout.Label (data.sfxCollisionBank.Count + " clip(s).", GUILayout.Width (100));
			}

			GUILayout.EndHorizontal(); //end line

			//make a field for every clip in the list that can be assigned
			for (int i = 0; i < data.sfxCollisionBank.Count; i++)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField("[" + i + "]:", GUILayout.Width (80));
				data.sfxCollisionBank[i] = EditorGUILayout.ObjectField(data.sfxCollisionBank[i], typeof(AudioClip), true, GUILayout.Width(180)) as AudioClip;
				EditorGUILayout.EndHorizontal();
			}

		}

		//headline for each data type
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("DEBUG", headlineStyle);
		EditorGUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		if (GUILayout.Button ("Regenerate Interface (if Surface changed)"))
		{
			sc.RegenerateSurfaceProperties();
		}
		GUILayout.EndHorizontal();

		//this will draw the default gui controls if not commented out (useful for debugging custom editor scripts)
		//base.OnInspectorGUI();
	}

	void CreateStyles()
	{
		defaultStyle = new GUIStyle();
		defaultStyle.alignment = TextAnchor.MiddleLeft;
		defaultStyle.normal.textColor = Color.white;
		
		headlineStyle = new GUIStyle();
		headlineStyle.fontStyle = FontStyle.Bold;
		headlineStyle.alignment = TextAnchor.MiddleCenter;
		headlineStyle.padding = new RectOffset(5, 0, 3, 2);
		headlineStyle.normal.textColor = Color.yellow;

		sectionStyle = new GUIStyle();
		sectionStyle.alignment = TextAnchor.MiddleLeft;
		sectionStyle.normal.textColor = Color.white;
	}
}
