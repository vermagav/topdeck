using UnityEngine;
using UnityEditor;
using System.Collections;

/// <summary>
/// Experiment with Custom Property Drawers
/// These are only usable for single line controls, as it turns out.
/// For multiple lines you need to write regular editor scripts.
/// </summary>

//[CustomPropertyDrawer (typeof (SurfaceSoundData))]
public class SurfaceSoundDataDrawer : PropertyDrawer {

	int idWidth = 40;
	int cueWidth = 60;

	public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label) {
		//SerializedProperty sfxCollisionBank = prop.FindPropertyRelative ("sfxCollisionBank");
		SerializedProperty sfxDraggingSound = prop.FindPropertyRelative ("sfxDraggingSound");

		EditorGUI.LabelField(new Rect(pos.x, pos.y, idWidth, pos.height), new GUIContent("SFX"));
		EditorGUI.LabelField(new Rect(pos.x + cueWidth, pos.y, cueWidth, pos.height), new GUIContent("Drag"));
		EditorGUI.PropertyField(new Rect(pos.x + idWidth + cueWidth, pos.y, pos.width - idWidth - cueWidth, pos.height), sfxDraggingSound);

		/*
		GUILayout.BeginHorizontal();
		GUILayout.Button("Load", GUILayout.Width(pos.width));
		GUILayout.EndHorizontal();
		*/


	}

	void nullFunction()
	{
	}


}

/*

public class ScaledCurveDrawer : PropertyDrawer {
    const int curveWidth = 50;
    const float min = 0;
    const float max = 1;
    public override void OnGUI (Rect pos, SerializedProperty prop, GUIContent label) {
        SerializedProperty scale = prop.FindPropertyRelative ("scale");
        SerializedProperty curve = prop.FindPropertyRelative ("curve");

        // Draw scale
        EditorGUI.Slider (
            new Rect (pos.x, pos.y, pos.width - curveWidth, pos.height),
            scale, min, max, label);

        // Draw curve
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        EditorGUI.PropertyField (
            new Rect (pos.width - curveWidth, pos.y, curveWidth, pos.height),
            curve, GUIContent.none);
        EditorGUI.indentLevel = indent;
    }
}
 */
