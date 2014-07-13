using UnityEngine;
using System.Collections;

public class FSM : MonoBehaviour {

	public enum State {
		Patrol,
		Chase,
		Return
	};

	public enum Trigger {
		EnemySighted,
		EnemyDisappeared,
		ReachedBase
	};

}
