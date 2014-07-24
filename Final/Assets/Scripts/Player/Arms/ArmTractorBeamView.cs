using UnityEngine;
using System.Collections;

public class ArmTractorBeamView : BaseRobotHand {

	public GameObject beamSource;

	[Range (0, 1)]
	public float pulseAmount;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void UpdateInputAxis(float axis)
	{
		//update beam view here
		pulseAmount = axis;
		beamSource.light.intensity = pulseAmount * 4;
	}
}
