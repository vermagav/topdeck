using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {

	//public bool flicker = false;
	public float randomOnTime = 0.5f;

	void Awake()
	{
		light.enabled = false;
	}

	void turnOn()
	{
		StartCoroutine(WaitTurnOn(Random.Range (0f, randomOnTime)));

	}

	IEnumerator WaitTurnOn(float waitTime) {
		yield return new WaitForSeconds(waitTime);

		light.enabled = true;
	}
}
