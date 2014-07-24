using UnityEngine;
using System.Collections;

public class LightControllerBCFix : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void turnOn() {
		GetComponent<Light>().intensity = 0.4f;
	}
}
