using UnityEngine;
using System.Collections;

public class BenReveal : MonoBehaviour {

	private bool isActive = false;
	
	public float fadeSpeed = 0.5f;          // Speed that the screen fades to and from black.
	private bool sceneStarting = true;      // Whether or not the scene is still fading in.
	private bool sceneEnding = false;		// Whether or not the scene is still fading out.

	public GUITexture guiTexture;
	
	void Activate() {
		if(isActive) {
			return;
		}

		// Show subtitles
		SubtitleManager.Instance.AddSubtitle ("Here lies Ben Copperbolt, Roboticist and Neuroscientist, 1974-2055 AD.", 15.0f);
		SubtitleManager.Instance.Play ();

		// Call fade to black
		sceneEnding = true;

		isActive = true;
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log ("Ben triggered!");
		if(other.tag == "Hand") {
			Debug.Log ("Inner tag triggered!");
			Activate();
		}
	}

	void Awake ()
	{
		// Set the texture so that it is the the size of the screen and covers it.
		guiTexture.pixelInset = new Rect(0f, 0f, Screen.width * 10, Screen.height * 10);

		// Fade the texture to clear.
		guiTexture.color = Color.clear;
		sceneStarting = false;
	}
	
	void Update ()
	{
		// Ending scene fade to black...
		if(sceneEnding)
			EndScene();
	}
	
	void FadeToBlack ()
	{
		// Lerp the colour of the texture between itself and black.
		guiTexture.color = Color.Lerp(guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
	}
	
	public void EndScene ()
	{
		// Make sure the texture is enabled.
		guiTexture.enabled = true;
		
		// Start fading towards black.
		FadeToBlack();
	}
}

