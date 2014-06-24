using UnityEngine;
using System.Collections;

public class SwayArm : MonoBehaviour, IArm {

	public float rotationLimit;
	public float rotationSpeed;

	private int upDown;

	private float currentRotation;

	bool armActivated = false;

	CreateSparks[] sparksEmitters;
	bool sparksFlying = false;

	void Awake()
	{
		currentRotation = 0f;
		upDown = -1;
		sparksEmitters = GetComponentsInChildren<CreateSparks>();
	}

	void FixedUpdate()
	{

		if(armActivated)
		{
			/*
			transform.Rotate (((float)upDown) * rotationSpeed * Time.deltaTime, 0f, 0f);
			currentRotation += ((float)upDown) * rotationSpeed * Time.deltaTime;
			
			if(Mathf.Abs(currentRotation) > rotationLimit)
			{
				upDown *= -1;
			}
			*/
			if (!sparksFlying)
			{
				foreach (CreateSparks spark in sparksEmitters)
				{
					spark.PlaySparks();
				}
				sparksFlying = true;
			}
		}
		else
		{
			float deltaRotation = currentRotation;
			currentRotation = Mathf.Lerp (currentRotation, 0, 0.3f);
			deltaRotation -= currentRotation;
			transform.Rotate ( -deltaRotation, 0f, 0f); //TODO: ISSUE: bug with improper rotation
			if (sparksFlying)
			{
				foreach (CreateSparks spark in sparksEmitters)
				{
					spark.KillSparks();
				}
				sparksFlying = false;
			}
		}

	}

	public void SetArmState(bool state)
	{
		armActivated = state;
	}

	public void SetArmAxis(float axis)
	{
		RotateArmTest(axis);
	}

	void RotateArmTest(float axis)
	{
		//Debug.LogError (axis);
		transform.Rotate (((float)upDown) * axis, 0f, 0f);
		currentRotation += ((float)upDown) * axis;
		
		if(Mathf.Abs(currentRotation) > rotationLimit)
		{
			upDown *= -1;
		}
	}
}
