/// <summary>
/// Simple Chase Camera Script
/// 
/// Follows the player but stays in the exact same facing.
/// Attach this to the main camera object, and enter the name of the player in goLookAt
/// in the inspector.
/// 
/// You may use this script for commercial purposes
/// 
/// http://www.lostmystic.com/2014/01/unity3d-chase-camera-script.html
/// 
/// http://lostmystic.com
/// Written in Unity 4.3.3
/// </summary>
using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	
	// How far away the camera will be, max from the player
	public float fDistance = 8f;
	public GameObject goLookAt;
	public float smooth = 10f;
	public Vector3 vOffset = new Vector3(0,1,0);
	
	public Vector3 cameraDistanceOffset = new Vector3();
	public Vector3 cameraPositionOffset = new Vector3();

	// Update is called once per frame
	void Update () {
		
		//float dist = Vector3.Distance(transform.position, goLookAt.transform.position);
		
		Vector3 vLook = goLookAt.transform.position + vOffset;
		
		// Reverse vector and extend into distance
		Vector3 vCamPos = goLookAt.transform.TransformDirection(Vector3.forward + cameraDistanceOffset);
		vCamPos *= -1;
		vCamPos *= fDistance;
		vCamPos += goLookAt.transform.position + cameraPositionOffset;
		
		// If lower than straight on, then simply look straight
		if (vCamPos.y < vLook.y) vCamPos.y = vLook.y;
		
		transform.position = vCamPos;
		
		// Rotates the camera to look at player
		transform.LookAt(vLook);
	}
	
}