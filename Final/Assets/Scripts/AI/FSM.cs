using UnityEngine;
using System.Collections;

public class FSM : MonoBehaviour {

	public enum State {
		Patrol,
		Chase,
		Return,
		Pacified
	};

	public enum Trigger {
		EnemySighted,
		EnemyDisappeared,
		ReachedBase,
		PacifyCondition
	};

}
