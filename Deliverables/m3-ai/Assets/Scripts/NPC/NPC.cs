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
		p = new NavMeshPath ();
	}

	void FixedUpdate()
	{

		//nav.SetDestination (location);


		if(nav.CalculatePath (location, p))
		{
			force = (p.corners[1] - transform.position);
			force.y = 0f;
			force = force.normalized * forceScale;
			rigidbody.AddForce(force);
		}
	}
}
