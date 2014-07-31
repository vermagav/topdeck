using UnityEngine;
using System.Collections;

public class ObjectHighlights : MonoBehaviour {

	public enum State
	{
		Original,
		Highlight
	}

	public State objectState = State.Original;

	public bool highlightPulse = false;
	public float pulseTime = 0.5f;

	public Color highlightColor = new Color(0, 97/255f, 1); //change this to robot "highlight" color
	public Color outlineColor = Color.white;

	[Range(0,1)]
	public float pinLightingOutline = 0.4f;

	[Range(0,0.1f)]
	public float vertexExtrusion = 0f;

	[Range(0,1)]
	public float highlightPower = 0;

	public Texture2D rampTexture;
	public Texture2D originalTexture;
	Shader highlightShader;
	Material highlightMaterial;
	Material originalMaterial;

	// Use this for initialization
	void Start () {
		highlightShader = Shader.Find("VGDCustom/HighlightPickup");
		rampTexture = Resources.Load("Textures/ObjectHighlightRamp") as Texture2D;
		originalMaterial = renderer.material;
		originalTexture = renderer.material.mainTexture as Texture2D;
		highlightMaterial = new Material(highlightShader);
		highlightMaterial.name = gameObject.name + "(H)";
		highlightMaterial.shader = highlightShader;
		highlightMaterial.SetTexture("_Ramp", rampTexture);
		highlightMaterial.SetTexture("_MainTex", originalTexture);
		renderer.material = highlightMaterial;
		objectState = State.Highlight;
		RefreshMaterial();
		RestoreOriginalMaterial();

	}
	
	// Update is called once per frame
	void Update () {
		if (highlightPulse && objectState == State.Highlight)
		{
			highlightPower = Mathf.PingPong (Time.time, pulseTime);
			//POLISH: look into Mathf.SmoothStep here
			RefreshMaterial();
		}
	}

	public void SetHighlight(bool state, float time)
	{
		if (objectState == State.Original)
			return;

		//time does nothing currently, but will animate the fading on/off in the future
		if (state)
		{
			highlightPower = 1;
		}
		else
		{
			highlightPower = 0;
		}
		RefreshMaterial();
	}

	public void SetOutline(bool state, float outlineWidth = 1)
	{
		//this is only for objects that have not been picked up yet
		if (objectState == State.Highlight)
			return;

		if (state)
		{
			renderer.material.SetColor("_OutlineColor", highlightColor);
		}
		else
			renderer.material.SetColor("_OutlineColor", Color.black);
	}

	void RefreshMaterial() {
		if (objectState == State.Highlight)
		{
			renderer.material.SetColor("_Color", highlightColor);
			renderer.material.SetColor("_OutlineColor", outlineColor);
			renderer.material.SetFloat("_Outline", pinLightingOutline);
			renderer.material.SetFloat("_Amount", vertexExtrusion);
			renderer.material.SetFloat("_CrossFade", highlightPower);
		}
	}

	void OnDestroy() {
		RestoreOriginalMaterial();
	}

	//for future use (dynamic highlights)
	public void RestoreOriginalMaterial() {
		if (objectState == State.Highlight)
		{
			renderer.material = originalMaterial;
			objectState = State.Original;
		}
	}

	public void RestoreHighlightMaterial() {
		if (objectState == State.Original)
		{
			renderer.material = highlightMaterial;
			objectState = State.Highlight;
		}
	}

}
