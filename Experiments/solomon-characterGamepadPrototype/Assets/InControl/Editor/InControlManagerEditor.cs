#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;
using InControl.ReorderableList;


namespace InControl
{
	[CustomEditor(typeof(InControlManager))]
	public class InControlManagerEditor : Editor
	{	
		SerializedProperty invertYAxis;
		SerializedProperty enableXInput;
		SerializedProperty useFixedUpdate;
		SerializedProperty customProfiles;
		Texture headerTexture;
		

		void OnEnable()
		{
			invertYAxis = serializedObject.FindProperty( "invertYAxis" );
			enableXInput = serializedObject.FindProperty( "enableXInput" );
			useFixedUpdate = serializedObject.FindProperty( "useFixedUpdate" );
			customProfiles = serializedObject.FindProperty( "customProfiles" );

			var path = AssetDatabase.GetAssetPath( MonoScript.FromScriptableObject( this ) );
			headerTexture = Resources.LoadAssetAtPath<Texture>( Path.GetDirectoryName( path ) + "/Images/InControlHeader.png" );
		}


		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			GUILayout.Space( 5.0f );

			var headerRect = GUILayoutUtility.GetRect( 0.0f, 5.0f );
			headerRect.width = headerTexture.width;
			headerRect.height = headerTexture.height;
			GUILayout.Space( headerRect.height );
			GUI.DrawTexture( headerRect, headerTexture );

			invertYAxis.boolValue = EditorGUILayout.ToggleLeft( "Invert Y Axis", invertYAxis.boolValue );
			enableXInput.boolValue = EditorGUILayout.ToggleLeft( "Enable XInput (Windows)", enableXInput.boolValue );
			useFixedUpdate.boolValue = EditorGUILayout.ToggleLeft( "Use Fixed Update", useFixedUpdate.boolValue );

			ReorderableListGUI.Title( "Custom Profiles" );
			ReorderableListGUI.ListField( customProfiles );

			GUILayout.Space( 3.0f );
			
			serializedObject.ApplyModifiedProperties();
		}
	}
}
#endif