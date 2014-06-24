using UnityEngine;
using System.Collections;

public class CollectableHighlights : MonoBehaviour {

	public GameObject highlightContainer;

	void Awake()
	{
		if (highlightContainer == null)
		{
			highlightContainer = transform.FindChild("CollectableHighlights").gameObject;
		}
	}

	public void RemoveHighlights()
	{
		if (highlightContainer != null)
		{
			Destroy (highlightContainer);
		}
	}
}
