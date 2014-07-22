using UnityEngine;
using System.Collections;

public class ObjectHighlights : MonoBehaviour {

	public Color highlightColor = Color.green;
	public Color outlineColor = Color.white;

	[Range(0,1)]
	public float pinLightingOutline = 0.4f;

	[Range(0,0.1f)]
	public float vertexExtrusion = 0f;

	[Range(0,1)]
	public float highlightPower = 0;

	public Texture2D rampTexture;
	Shader highlightShader;
	Material highlightMaterial;

	// Use this for initialization
	void Start () {
		highlightShader = Shader.Find("VGDCustom/HighlightPickup");
		rampTexture = Resources.Load("Textures/ObjectHighlightRamp") as Texture2D;
		highlightMaterial = new Material(highlightShader);
		renderer.material.name = "Toon Highlight";
		renderer.material.shader = highlightShader;
		renderer.material.SetTexture("_Ramp", rampTexture);
		RefreshMaterial();

	}
	
	// Update is called once per frame
	void Update () {
		//highlightPower = Mathf.PingPong(Time.time, 1);
		//RefreshMaterial();
	
	}

	public void SetHighlight(bool state, float time)
	{
		//time does nothing currently, but will animate the fading on/off in the future
		if (state)
			highlightPower = 1;
		else
			highlightPower = 0;
		RefreshMaterial();

	}

	void RefreshMaterial() {
		renderer.material.SetColor("_Color", highlightColor);
		renderer.material.SetColor("_OutlineColor", outlineColor);
		renderer.material.SetFloat("_Outline", pinLightingOutline);
		renderer.material.SetFloat("_Amount", vertexExtrusion);
		renderer.material.SetFloat("_CrossFade", highlightPower);
	}
}
