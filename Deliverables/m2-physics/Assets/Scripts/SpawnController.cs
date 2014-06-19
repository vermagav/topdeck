using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour {

	GameObject player;
	GameObject spawn;
	Dictionary<KeyCode, string> spawnList;

	void Start () {
		spawnList = new Dictionary<KeyCode, string> ();
		spawnList.Add(KeyCode.Alpha1, "Spawn1");
		spawnList.Add(KeyCode.Alpha2, "Spawn2");
		spawnList.Add(KeyCode.Alpha3, "Spawn3");

		player = GameObject.FindGameObjectWithTag("Player");
		spawn = GameObject.FindGameObjectWithTag(spawnList[KeyCode.Alpha1]);
	}
	
	void Update () {
		foreach (KeyValuePair<KeyCode, string> entry in spawnList) {
			if (Input.GetKey(entry.Key)) {
				spawn = GameObject.FindGameObjectWithTag(entry.Value);
				player.transform.position = spawn.transform.position;
			}
		}
	}

	void PlaySpawnSound (KeyCode key) {
		// TODO
	}
}
