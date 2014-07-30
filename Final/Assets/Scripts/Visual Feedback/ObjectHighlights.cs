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

	public Color highlightColor = new Color(0, 97/255f, 1);
	public Color outlineColor = Color.white;

	[Range(0,1)]
	public float pinLightingOutline = 0.4f;

	[Range(0,0.1f)]
	public float vertexExtrusion = 0f;

	[Range(0,1)]
	public float highlightPower = 0;

	public Texture2D rampTexture;
	Texture2D originalTexture;
	Shader highlightShader;
	Material highlightMaterial;
	Material originalMaterial;

	// Use this for initialization
	void Start () {
		highlightShader = Shader.Find("VGDCustom/HighlightPickup");
		rampTexture = Resources.Load("Textures/ObjectHighlightRamp") as Texture2D;
		originalMaterial = renderer.material;
		originalTexture = (Texture2D)renderer.material.mainTexture;
		highlightMaterial = new Material(highlightShader);
		renderer.material = highlightMaterial;
		renderer.material.name = "Toon Highlight";
		renderer.material.shader = highlightShader;
		renderer.material.SetTexture("_Ramp", rampTexture);
		renderer.material.mainTexture = originalTexture;
		objectState = State.Highlight;
		RefreshMaterial();
		RestoreOriginalMaterial();

	}
	
	// Update is called once per frame
	void Update () {
		if (highlightPulse)
		{
			highlightPower = Mathf.PingPong(Time.time, 1);
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

	void RefreshMaterial() {
		renderer.material.SetColor("_Color", highlightColor);
		renderer.material.SetColor("_OutlineColor", outlineColor);
		renderer.material.SetFloat("_Outline", pinLightingOutline);
		renderer.material.SetFloat("_Amount", vertexExtrusion);
		renderer.material.SetFloat("_CrossFade", highlightPower);
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
