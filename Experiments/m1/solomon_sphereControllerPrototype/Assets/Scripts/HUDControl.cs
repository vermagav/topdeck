using UnityEngine;
using System.Collections;

public class HUDControl : MonoBehaviour {

	public GameObject SphereController;
	ISphereController Controller;

	public GUIText ControllerMode;
	public GUIText Watermark;

	// Use this for initialization
	void Start () {
		Controller = (ISphereController)SphereController.GetComponent(typeof(ISphereController));
	}
	
	// Update is called once per frame
	void Update () {
		if (Controller == null)
			return;
		ControllerMode.text = ((SphereController.Mode)Controller.GetControllerMode()).ToString();
	}
}
