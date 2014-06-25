using UnityEngine;
using System.Collections;

public class TelescopeArm : MonoBehaviour, IArm {

	public float moveSpeed = 0.3f;
	public float maxMove = 1f;
	public float maxExtend = 5f;
	public GameObject hand;
	public GameObject piston;
	
	private Vector3 desiredPosition;
	private Vector3 restingPosition;


	void Awake()
	{
		desiredPosition = new Vector3(0f, 0f, 0f);
		restingPosition = hand.transform.localPosition;
	}

	void FixedUpdate()
	{
		extendArm ();
	}
	
	private void extendArm()
	{
		//hand.transform.localPosition = restingPosition + desiredPosition;

		hand.transform.localPosition = Vector3.Lerp (hand.transform.localPosition, restingPosition + desiredPosition, moveSpeed);
		piston.transform.localPosition = hand.transform.localPosition / 2f;
		piston.transform.localScale = new Vector3(0.3f, hand.transform.localPosition.z / 2f, 0.3f);//hand.transform.localPosition.z / 2f);
	}
	
	public void SetArmState(bool state)
	{
		//send a message to Rob's hand implementation
	}
	
	public void SetArmAxis(float axis)
	{
		//telescope the arm
		desiredPosition.z = axis * maxExtend;
		//Debug.Log (desiredPosition);
	}
}
