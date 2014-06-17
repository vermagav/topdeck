#define PROTOTYPE
#define PROTOTYPE
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using ProBuilder2.Common;
using ProBuilder2.MeshOperations;
using ProBuilder2.EditorEnum;

public class ExpandSelection : Editor
{

	[MenuItem("Tools/" + pb_Constant.PRODUCT_NAME + "/Selection/Grow Selection Plane &g", true, pb_Constant.MENU_SELECTION + 2)]
	[MenuItem("Tools/" + pb_Constant.PRODUCT_NAME + "/Selection/Grow Selection %&g", true, pb_Constant.MENU_SELECTION + 1)]
	public static bool VerifySelectionCommand()
	{
		return pb_Editor.instanceIfExists != null;
	}

	[MenuItem("Tools/" + pb_Constant.PRODUCT_NAME + "/Selection/Grow Selection #&g", false, pb_Constant.MENU_SELECTION + 1)]
	public static void MenuGrowSelection()
	{
		foreach(pb_Object pb in pbUtil.GetComponents<pb_Object>(Selection.transforms))
		{
			switch(pb_Editor.instance.selectionMode)
			{
				case SelectMode.Vertex:
				case SelectMode.Edge:
					pb.SetSelectedEdges(pbMeshUtils.GetConnectedEdges(pb, pb.SelectedTriangles));
					break;
				
				case SelectMode.Face:
					List<pb_Face> all = pbMeshUtils.GetConnectedFaces(pb, pb.SelectedFaces);
					pb.SetSelectedFaces(all.ToArray());

					break;
			}
		}
		pb_Editor.instance.UpdateSelection();
	}
	
	[MenuItem("Tools/" + pb_Constant.PRODUCT_NAME + "/Selection/Grow Selection Plane &g", false, pb_Constant.MENU_SELECTION + 2)]
	public static void MenuGrowSelectionPlanar()
	{
		foreach(pb_Object pb in pbUtil.GetComponents<pb_Object>(Selection.transforms))
		{
			List<pb_Face> newFaceSelection = new List<pb_Face>( pb.SelectedFaces );

			foreach(pb_Face f in pb.SelectedFaces)
			{
				Vector3 nrm = pb.FaceNormal(f);

				List<pb_Face> adjacent = pbMeshUtils.GetConnectedFaces(pb, f);

				foreach(pb_Face connectedFace in adjacent)
				{
					float dot = Vector3.Dot(nrm, pb.FaceNormal(connectedFace));					
					
					if( dot > .9f )
						newFaceSelection.Add(connectedFace);
				}
			}

			pbUndo.RecordObject(pb, "Grow Selection");
			pb.SetSelectedFaces(newFaceSelection.Distinct().ToArray());
			pb_Editor.instance.UpdateSelection();
		}
	}
}
