#define PROTOTYPE
#define PROTOTYPE
using UnityEngine;
using System.Collections;
using UnityEditor;

namespace ProBuilder2.Actions
{
	public class CleanUpLeakedMeshes : Editor
	{
		[MenuItem("Tools/" + pb_Constant.PRODUCT_NAME + "/Repair/Clean Up Leaked Meshes")]
		public static void CleanUp()
		{
			if(EditorUtility.DisplayDialog("Clean Up Leaked Meshes?",
				"Cleaning leaked meshes will permenantly delete any deleted pb_Objects, are you sure you don't want to undo?", "Clean Up", "Stay Dirty"))
			{
				EditorUtility.UnloadUnusedAssetsIgnoreManagedReferences();
			}
		}
	}
}
