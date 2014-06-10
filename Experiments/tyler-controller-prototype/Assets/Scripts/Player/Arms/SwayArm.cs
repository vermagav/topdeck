using UnityEngine;
using System.Collections;

public class SwayArm : MonoBehaviour {

	public float rotationLimit;
	public float rotationSpeed;

	private int upDown;

	private float currentRotation;
	
	void Awake()
	{
		currentRotation = 0f;
		upDown = -1;
	}

	void FixedUpdate()
	{

		if(Input.GetButton("Fire2"))
		{
			transform.Rotate (((float)upDown) * rotationSpeed * Time.deltaTime, 0f, 0f);
			currentRotation += ((float)upDown) * rotationSpeed * Time.deltaTime;
			
			if(Mathf.Abs(currentRotation) > rotationLimit)
			{
				upDown *= -1;
			}
		}
		else
		{
			float deltaRotation = currentRotation;
			currentRotation = Mathf.Lerp (currentRotation, 0, 0.3f);
			deltaRotation -= currentRotation;
			transform.Rotate ( -deltaRotation, 0f, 0f);
		}


	}
}
