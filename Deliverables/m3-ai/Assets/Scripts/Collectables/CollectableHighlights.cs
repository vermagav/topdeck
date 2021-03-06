﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class CollectableHighlights : MonoBehaviour {

	public GameObject highlightContainer;
	SphereCollider collectableRangeCollider;

	void Awake()
	{
		//TODO make this more flexible, assumes it is present and isTrigger is set to true
		collectableRangeCollider = GetComponent<SphereCollider>();

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
		Destroy (this);
	}

	void OnDestroy()
	{
		if (collectableRangeCollider != null)
		{
			Destroy (collectableRangeCollider);
		}
	}

}
