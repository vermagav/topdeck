using UnityEngine;
using System.Collections;

public class ArmTractorBeamView : BaseRobotHand {

	public GameObject beamSource;
	public LightningBolt lightning;
	ParticleRenderer particleRenderer;
	public Transform defaultArc;
	float lightningScale = 0;

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
				lightningScale = 0.01f + (0.02f * pulseAmount);
			}
			else
			{
				lightningScale = 0.01f * pulseAmount;
			}
			particleRenderer.maxParticleSize = lightningScale;
			if (audio)
			{
				audio.volume = lightningScale / 0.03f;
				if (audio.volume == 0)
					audio.Stop();
				else
					audio.Play();
			}
		}

	}

	void SetTarget(Transform target)
	{
		if (lightning)
		{
			lightning.target = target;
			if (audio)
				audio.pitch = 0.7f;
		}
	}

	void SetDefaultTarget(Transform alternativeTarget)
	{
		if (lightning)
		{
			if (defaultArc)
			{
				lightning.target = defaultArc;
				if (audio)
					audio.pitch = 1;
			}
			else
				lightning.target = alternativeTarget;
		}
	}

}
