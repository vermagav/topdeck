﻿using UnityEngine;
using System.Collections;

public class PlayerArmSocket : MonoBehaviour {

	public GameObject playerDriveTrain;
	public GameObject playerTorso;
	
	public GameObject acquiredBuff;
	public GameObject connector;

	public IArm currentArm;

	void Update()
	{
		transform.position = playerDriveTrain.transform.position;
	}

	void OnTriggerEnter (Collider other)
	{
		//Debug.Log ("Pickup Arm Collided");
		if(other.gameObject.CompareTag("Arm"))
		{
			CollectableHighlights collectable = other.gameObject.GetComponent<CollectableHighlights>();
			if (collectable != null)
			{
				collectable.RemoveHighlights();
				currentArm = (IArm)other.gameObject.GetComponent(typeof(IArm));
				SetupArm();

			}
			/*
			else //legacy code for old prefabs
			{
				Destroy(other.gameObject);
				GameObject newArm = (GameObject)Instantiate(acquiredBuff, connector.transform.position, connector.transform.rotation);
				newArm.transform.parent = connector.transform;
				//TODO: Check what kind of arm it is, we know it is a sway arm currently
				//(ISphereController)transform.parent.GetComponent(typeof(ISphereController));
				currentArm = (IArm)newArm.GetComponent(typeof(IArm));
			}
			*/
		}
	}

	public void SetArmState(bool state)
	{
		if (currentArm != null)
		{
			Component arm = (Component)currentArm;
			arm.SendMessage("SetArmState", state);
		}
	}

	public void SetArmAxis(float axis)
	{
		if (currentArm != null)
		{
			//Debug.Log("setArmAxis to:" + axis);
			Component arm = (Component)currentArm;
			arm.SendMessage("SetArmAxis", axis);
		}
	}

	void SetupArm()
	{
		if (currentArm != null)
		{
			Component arm = (Component)currentArm;
			arm.SendMessage("Setup", SendMessageOptions.DontRequireReceiver);
			arm.transform.position = connector.transform.position;
			arm.transform.rotation = connector.transform.rotation;
			arm.transform.parent = connector.transform;

			FixedJoint fixedJoint = arm.gameObject.AddComponent<FixedJoint>();
			fixedJoint.connectedBody = playerTorso.rigidbody;


		}
	}
}
