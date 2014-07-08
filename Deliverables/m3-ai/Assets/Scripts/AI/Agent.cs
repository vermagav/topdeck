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
	public LineRenderer debugLine;

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
			} else if (trigger == FSM.Trigger.EnemySighted) {
				// Eneemy sighted, transition to chase mode
				currentState = FSM.State.Chase;
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
			MoveAgent( waypointList[nextWaypoint].position );
			break;
			
		case FSM.State.Chase:
			// Chase the player
			MoveAgent( player.transform.position );
			break;
			
		case FSM.State.Return:
			// Return home
			MoveAgent( waypointList[0].position );
			if (Vector3.Distance (this.transform.position, waypointList[0].position) <= 2.0f) {
				Transition(FSM.Trigger.ReachedBase);
			}
			break;
		};
	}

	void MoveAgent (Vector3 destination) {
		// Move navMeshAgent
		navMeshAgent.SetDestination (destination);

		// Render line in game view to see agent target
		Vector3 lineTarget = destination;
		lineTarget.y = this.transform.position.y;
		debugLine.SetPosition(0, this.transform.position);
		debugLine.SetPosition(1, lineTarget);
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
