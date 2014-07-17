using UnityEngine;
using System.Collections;

public class GUIController : MonoBehaviour {

	public float bannerWidth;
	public float bannerHeight;

	public int currentMessage;
	public bool fadingIn;
	private Color color;

	private string[] message = {"<size=15>ROOM 1:</size>\n<size=30>REBIRTH</size>", "<size=15>ROOM 2:</size>\n<size=30>CONSTRUCTION</size>", "<size=15>ROOM 3:</size>\n<size=30>INTRUDER</size>", "<size=15>ROOM 4:</size>\n<size=30>FINALE: To be revealed July 31st...</size>"};

	public void showBanner(int banner)
	{
		currentMessage = banner;
		fadingIn = true;
	}

	void Awake()
	{
		color = Color.white;//GUI.color;
	}

	void OnGUI()
	{
		if(fadingIn)
		{
			color.a = Mathf.Lerp(color.a, 1f, 0.5f * Time.deltaTime);
			GUI.color = color;
			if(GUI.color.a > 0.99f)
			{
				fadingIn = false;
			}
		}
		else
		{
			color.a = Mathf.Lerp(color.a, 0f, 0.5f * Time.deltaTime);
			GUI.color = color;		
		}
		if(GUI.color.a > 0.01f)
		{
			GUI.Box(new Rect((Screen.width - bannerWidth)/2f,(Screen.height - bannerHeight)/2f, bannerWidth, bannerHeight), message[currentMessage]);
		}
	}

}
