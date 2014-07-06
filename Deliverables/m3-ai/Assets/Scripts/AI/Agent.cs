using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Agent : MonoBehaviour {
	
	private FSM.State currentState;
	private NavMeshAgent navMeshAgent;

	// Public list of waypoint transforms inserted via Unity inspector
	public Transform[] waypointList;

	// Index of the next waypoint to move towards
	private int nextWaypoint;

	public GameObject player;
	public int chaseDistanceThreshold;

	void Start () {
		navMeshAgent = GetComponent<NavMeshAgent> ();

		// Set default state to patrol
		currentState = FSM.State.Patrol;

		// Set index to default
		nextWaypoint = 0;

		// Sanity check
		if( waypointList.Length == 0 ) {
			Debug.Log("ERROR: the waypoint list is empty. Check to see if waypoints were added via Unity inspector.");
			return;
		}
	}
	
	void Transition(FSM.Trigger trigger) {
		switch (currentState) {
		case FSM.State.Patrol:
			if (trigger == FSM.Trigger.EnemySighted) {
				// Enemy sighted, transition to chase mode
				currentState = FSM.State.Chase;
			}
			break;
		
		case FSM.State.Chase:
			if (trigger == FSM.Trigger.EnemyDisappeared) {
				// Enemy disappeared, transition to return mode
				currentState = FSM.State.Return;
			}
			break;
		
		case FSM.State.Return:
			if (trigger == FSM.Trigger.ReachedBase) {
				// Reached base, transition to patrol mode
				currentState = FSM.State.Patrol;
			}
			break;
		};
	}
	
	void ProcessState() {
		switch (currentState) {
		case FSM.State.Patrol:
			// Cycle through waypoints
			if( Vector3.Distance(this.transform.position, waypointList[nextWaypoint].position) <= 2.0f ) {
				nextWaypoint++;
				if( nextWaypoint > waypointList.Length - 1) {
					nextWaypoint = 0;
				}
			}
			// Move towards next target
			navMeshAgent.SetDestination( waypointList[nextWaypoint].position );
			break;
			
		case FSM.State.Chase:
			// Chase the player
			navMeshAgent.SetDestination( player.transform.position );
			break;
			
		case FSM.State.Return:
			// Return home
			navMeshAgent.SetDestination( waypointList[0].position );
			if (Vector3.Distance (this.transform.position, waypointList[0].position) <= 2.0f) {
				Transition(FSM.Trigger.ReachedBase);
			}
			break;
		};
	}

	void Update () {
		// Process current FSM state
		ProcessState ();

		// State transitions for enemy sightings
		if( Vector3.Distance(this.transform.position, player.transform.position) <= chaseDistanceThreshold ) {
			Transition(FSM.Trigger.EnemySighted);
		} else {
			Transition(FSM.Trigger.EnemyDisappeared);
		}
	}
}
