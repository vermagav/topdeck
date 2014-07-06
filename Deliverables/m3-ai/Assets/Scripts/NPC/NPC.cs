using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour {

	public Vector3 location;
	public float forceScale;
	private NavMeshAgent nav;
	private Vector3 force;
	private NavMeshPath p;

	void Awake()
	{
		nav = GetComponent<NavMeshAgent> ();
		nav.SetDestination (location);
		//nav.Stop ();
	}

	void FixedUpdate()
	{

		nav.SetDestination (location);


		//if(nav.CalculatePath (location, p))
		//{
			//force = (p.corners [0] - transform.position) * forceScale;
			//Debug.Log (p);
		//}
		//rigidbody.AddForce(nav.path.corners [0]);
	}
}
