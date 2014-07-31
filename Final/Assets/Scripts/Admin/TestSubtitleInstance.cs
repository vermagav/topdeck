using UnityEngine;
using System.Collections;

public class TestSubtitleInstance : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Test ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Test()
	{
		SubtitleManager.Instance.AddSubtitle ("OH MY GOD...", 1f);
		SubtitleManager.Instance.AddSubtitle ("A BOX.", 1f);
		SubtitleManager.Instance.AddSubtitle ("<3 <3 <3 <3 <3", 2f, Color.magenta);
	}
}
