using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(PlayerMovement))]
public class Agent : MonoBehaviour {
	
	private FSM.State currentState;
	private NavMeshAgent navMeshAgent;
	
	// Public list of waypoint transforms inserted via Unity inspector
	public Transform[] waypointList;

	public Transform pushEntrance;
	
	// Index of the next waypoint to move towards
	private int nextWaypoint;
	
	public GameObject player;
	public int chaseDistanceThreshold;
	public int caughtPlayerDistanceThreshold;
	public LineRenderer debugLine;
	public PlayerMovement playerMovement;

	public AudioClip soundAlert;
	public AudioClip soundLaugh;
	public AudioClip soundLament;
	private int numReturnAfterAttacking;

	public float timeSinceLastAttack;
	private const float attackCooldown = 1.0f;

	void Start () {
		navMeshAgent = GetComponent<NavMeshAgent> ();
		
		// Set default state to patrol
		currentState = FSM.State.Patrol;
		
		// Set defaults
		nextWaypoint = 0;
		timeSinceLastAttack = 1.0f;
		numReturnAfterAttacking = 0;
		
		// Sanity check
		if( waypointList.Length == 0 ) {
			Debug.Log("ERROR: the waypoint list is empty. Check to see if waypoints were added via Unity inspector.");
			return;
		}

		if (playerMovement == null) {
			playerMovement = GetComponent<PlayerMovement>();
		}
	}

	bool CanAttack() {
		if (timeSinceLastAttack >= attackCooldown) {
			return true;
		} else {
			timeSinceLastAttack += Time.deltaTime;
			return false;
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
			if ( Vector3.Distance(this.transform.position, player.transform.position) <= caughtPlayerDistanceThreshold ) {
				if(CanAttack()) {
					player.rigidbody.AddForce((pushEntrance.position - player.transform.position) * 30, ForceMode.Impulse);
					Transition(FSM.Trigger.EnemyDisappeared);
					return;
				}
			}
			break;
			
		case FSM.State.Return:
			// Return home
			MoveAgent( waypointList[0].position );
			if (Vector3.Distance (this.transform.position, waypointList[0].position) <= 2.0f) {
				numReturnAfterAttacking++;
				if(numReturnAfterAttacking % 2 == 1) {
					audio.clip = soundLaugh;
				} else {
					audio.clip = soundLament;
				}
				audio.Play ();
				Transition(FSM.Trigger.ReachedBase);
			}
			break;
		};
	}
	
	void MoveAgent (Vector3 destination) {
		Vector3 newDestination = destination;
		newDestination.y = this.transform.position.y;
		
		// If in chase mode, predict where the player is headed and try to head off
		if (currentState == FSM.State.Chase) {
			const float interceptDistance = 0.5f;
			newDestination += (player.transform.forward + playerMovement.GetTargetVelocity()) * interceptDistance;
		}
		
		// Render line in game view to see agent target
		debugLine.SetPosition(0, this.transform.position);
		debugLine.SetPosition(1, newDestination);
		
		// Move navMeshAgent
		navMeshAgent.SetDestination (newDestination);
	}
	
	void Update () {
		// Process current FSM state
		ProcessState ();
		
		// State transitions for enemy sightings
		if (player.transform.position.z < pushEntrance.position.z) {
			Transition(FSM.Trigger.EnemyDisappeared);
		} else if ( Vector3.Distance(this.transform.position, player.transform.position) <= chaseDistanceThreshold ) {
			if(!audio.isPlaying) {
				audio.clip = soundAlert;
				audio.Play();
			}
			Transition(FSM.Trigger.EnemySighted);
		}
	}
}
