using UnityEngine;
using System.Collections;

public class SphereViewSimple : MonoBehaviour {

	public ISphereController Controller;

	public float RotationScale = 60f;

	public Material LightMaterial;
	public Material HeavyMaterial;

	// Use this for initialization
	void Start () {
		if (Controller == null)
		{
			Controller = (ISphereController)transform.parent.GetComponent(typeof(ISphereController));
		}
		if (Controller == null)
		{
			Debug.LogError("Couldn't grab ISphereController.");
		}
	}

	void FixedUpdate () {
		//the view should follow the position of the controller
		transform.position = Controller.GetCurrentPosition();

		RotateView (Controller.GetMovement());

		UpdateMaterialFromMode();


	}

	public void RotateView(Vector3 sphereMovement)
	{
		//calculating the 90 degree angle to the current movement vector
		Vector3 rotationAxis = new Vector3(sphereMovement.z, 0, -sphereMovement.x);

		//rotating around this axis by the magnitude of the movement vector
		transform.Rotate (rotationAxis, sphereMovement.magnitude * RotationScale * Controller.GetRotationSpeed(), Space.World);

	}

	void UpdateMaterialFromMode()
	{
		switch ((SphereController.Mode)Controller.GetControllerMode ())
		{
		case SphereController.Mode.Heavy:
			renderer.material = HeavyMaterial;
			break;
		case SphereController.Mode.Light:
			renderer.material = LightMaterial;
			break;
		}
	}
}
