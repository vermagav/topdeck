using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Agent : MonoBehaviour {
	
	private FSM.State currentState;
	private NavMeshAgent navMeshAgent;
	
	// Public list of waypoint transforms inserted via Unity inspector
	public Transform[] waypointList;

	public Transform pushEntrance;
	
	// Index of the next waypoint to move towards
	private int nextWaypoint;
	
	public GameObject player;
	public GameObject pacifyObject;
	public GrabbableItem grabbableItem;
	public int chaseDistanceThreshold;
	public int caughtPlayerDistanceThreshold;
	public PlayerMovement playerMovement;
	public Light agentLight;

	public AudioClip soundAlert;
	public AudioClip soundLaugh;
	public AudioClip soundLament;
	public AudioClip soundHappy;
	private bool happySoundPlayed;
	private int numReturnAfterAttacking;

	public float timeSinceLastAttack;
	private const float attackCooldown = 1.0f;

	void Start () {
		navMeshAgent = GetComponent<NavMeshAgent> ();
		
		// Set default state to patrol
		SwitchState(FSM.State.Patrol);
		
		// Set defaults
		nextWaypoint = 0;
		timeSinceLastAttack = 1.0f;
		numReturnAfterAttacking = 0;
		happySoundPlayed = false;
		
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
				SwitchState(FSM.State.Chase);
			} else if (trigger == FSM.Trigger.PacifyCondition) {
				SwitchState(FSM.State.Pacified);
			}
			break;
			
		case FSM.State.Chase:
			if (trigger == FSM.Trigger.EnemyDisappeared) {
				// Enemy disappeared, transition to return mode
				SwitchState(FSM.State.Return);
			} else if (trigger == FSM.Trigger.PacifyCondition) {
				SwitchState(FSM.State.Pacified);
			}
			break;
			
		case FSM.State.Return:
			if (trigger == FSM.Trigger.ReachedBase) {
				// Reached base, transition to patrol mode
				SwitchState(FSM.State.Patrol);
			} else if (trigger == FSM.Trigger.EnemySighted) {
				// Eneemy sighted, transition to chase mode
				SwitchState(FSM.State.Chase);
			} else 	if (trigger == FSM.Trigger.PacifyCondition) {
				SwitchState(FSM.State.Pacified);
			}
			break;

		case FSM.State.Pacified:
			// Once pacified, do nothing.
			break;
		};
	}

	void SwitchState(FSM.State newState) {
		currentState = newState;
		switch (currentState) {
		case FSM.State.Patrol:
			agentLight.color = Color.white;
			break;
			
		case FSM.State.Chase:
			agentLight.color = Color.red;
			break;
			
		case FSM.State.Return:
			agentLight.color = Color.white;
			break;
			
		case FSM.State.Pacified:
			agentLight.color = Color.magenta;
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
					player.rigidbody.AddForce((pushEntrance.position - player.transform.position) * 60, ForceMode.Impulse);
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
					SubtitleManager.Instance.AddSubtitle ("Ha. Ha.", 1.5f, Color.red);
				} else {
					audio.clip = soundLament;
					SubtitleManager.Instance.AddSubtitle ("I wish I had a box.", 1.5f, Color.red);
				}
				audio.Play();
				SubtitleManager.Instance.Play ();
				Transition(FSM.Trigger.ReachedBase);
			}
			break;

		case FSM.State.Pacified:
			MoveAgent( pacifyObject.transform.position );
			if (Vector3.Distance (this.transform.position, pacifyObject.transform.position) <= caughtPlayerDistanceThreshold) {
				navMeshAgent.Stop (true);
				if (grabbableItem != null)
				{
					grabbableItem.SetGrabbable(false);
					grabbableItem.ReleaseItem();
					pacifyObject.rigidbody.isKinematic = true;
					Destroy (grabbableItem);
				}

			}
			if (!happySoundPlayed && !audio.isPlaying) {
				audio.clip = soundHappy;
				audio.Play();
				happySoundPlayed = true;
				SubtitleManager.Instance.Stop ();
				SubtitleManager.Instance.AddSubtitle ("Oh my god. A box! <3", 2.0f, Color.magenta);
				SubtitleManager.Instance.Play ();
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

		// Move navMeshAgent
		navMeshAgent.SetDestination (newDestination);
	}
	
	void Update () {
		// Process current FSM state
		ProcessState ();
		
		// State transitions for enemy sightings
		if (Vector3.Distance (this.transform.position, pacifyObject.transform.position) <= chaseDistanceThreshold) {
			Transition (FSM.Trigger.PacifyCondition);
		} else if (player.transform.position.z < pushEntrance.position.z) {
			Transition(FSM.Trigger.EnemyDisappeared);
		} else if (Vector3.Distance(this.transform.position, player.transform.position) <= chaseDistanceThreshold) {
			if(!audio.isPlaying) {
				audio.clip = soundAlert;
				audio.Play();
				SubtitleManager.Instance.Stop ();
				SubtitleManager.Instance.AddSubtitle ("Intruder Alert!", 1.0f, Color.red);
				SubtitleManager.Instance.Play ();

			}
			Transition(FSM.Trigger.EnemySighted);
		}
	}
}
