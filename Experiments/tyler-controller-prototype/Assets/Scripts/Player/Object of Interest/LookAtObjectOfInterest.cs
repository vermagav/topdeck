﻿using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class LookAtObjectOfInterest : MonoBehaviour {

	public float lookSpeed;
	public float lookAngle;
	public float lookMinDistance;
	
	private GameObject currentItemOfInterest;
	private Quaternion lastRotation;
	private Quaternion targetRotation;
	private float lookMinDistanceSquared;

	void Awake()
	{
		lastRotation = transform.rotation;
		targetRotation = transform.rotation;
		lookMinDistanceSquared = Mathf.Pow (lookMinDistance, 2);
	}

	void FixedUpdate()
	{
		if(currentItemOfInterest == null)
		{
			targetRotation = transform.parent.rotation;
		}
		else
		{
			targetRotation = Quaternion.LookRotation(currentItemOfInterest.transform.position - transform.position);
		}



		rotateToTarget ();
		currentItemOfInterest = null;
	}

	void OnTriggerStay(Collider other) {
		if(("ObjectOfInterest").Equals(Regex.Replace(other.gameObject.name, @"\_(\(.*\))", "")));
		{
			Vector3 deltaPosition = other.transform.position - transform.position;
			Quaternion possibleLookRotation = Quaternion.LookRotation(deltaPosition);
			if(Quaternion.Angle(transform.parent.rotation, possibleLookRotation) <= lookAngle)
			{

				if(Mathf.Pow(deltaPosition.x, 2) + Mathf.Pow(deltaPosition.z, 2) > lookMinDistanceSquared)
				{
					if(currentItemOfInterest == null || 
					   currentItemOfInterest.GetComponent<InterestLevel>().levelOfInterest 
					   < other.gameObject.GetComponent<InterestLevel>().levelOfInterest)
					{
						currentItemOfInterest = other.gameObject;
					}
				}
			}

		}
	}

	private void rotateToTarget()
	{
		transform.rotation = Quaternion.Lerp (lastRotation, targetRotation, lookSpeed);
		lastRotation = transform.rotation;
	}
}
