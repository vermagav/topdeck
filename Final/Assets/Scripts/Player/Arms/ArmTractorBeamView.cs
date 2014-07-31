using UnityEngine;
using System.Collections;

public class ArmTractorBeamView : BaseRobotHand {

	public GameObject beamSource;
	public LightningBolt lightning;
	ParticleRenderer particleRenderer;

	[Range (0, 1)]
	public float pulseAmount;

	// Use this for initialization
	void Start () {
		if (lightning)
			particleRenderer = lightning.GetComponent<ParticleRenderer>();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void UpdateInputAxis(float axis)
	{
		//update beam view here
		pulseAmount = axis;
		if (beamSource)
			beamSource.light.intensity = pulseAmount * 4;
		if (lightning)
		{
			if (lightning.target.CompareTag("Collectable"))
			{
				particleRenderer.maxParticleSize = 0.01f + (0.02f * pulseAmount);
			}
			else
			{
				particleRenderer.maxParticleSize = 0.01f * pulseAmount;
			}
		}

	}

	void SetTarget(Transform target)
	{
		if (lightning)
		{
			lightning.target = target;
		}
	}
}
