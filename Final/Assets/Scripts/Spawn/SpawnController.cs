using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnController : MonoBehaviour {

	GameObject player;
	GameObject spawn;
	Dictionary<KeyCode, SpawnData> spawnList;

	public GUIText displayText;

	void Start () {
		spawnList = new Dictionary<KeyCode, SpawnData> ();
		PopulateSpawnData();

		player = GameObject.FindGameObjectWithTag("Player");
		spawn = GameObject.FindGameObjectWithTag(spawnList[KeyCode.Alpha1].SpawnTag());
	}

	void Update () {
		foreach (KeyValuePair<KeyCode, SpawnData> entry in spawnList) {
			if (Input.GetKey(entry.Key)) {
				Spawn (entry.Value);
			}
		}
	}

	void PopulateSpawnData () {
		spawnList.Add(KeyCode.Alpha1, new SpawnData("Spawn1", "Garden 1: Factory, by Tyler Labean", null));
		spawnList.Add(KeyCode.Alpha2, new SpawnData("Spawn2", "Garden 2: Park, by Rob Solomon", null));
		spawnList.Add(KeyCode.Alpha3, new SpawnData("Spawn3", "Garden 3: Arena, by Gav Verma", null));
	}

	void Spawn (SpawnData data) {
		// Fetch correct spawn tag
		spawn = GameObject.FindGameObjectWithTag(data.SpawnTag());

		// Move player to new garden
		player.transform.position = spawn.transform.position;

		// Update HUD text
		displayText.text = data.DisplayText ();

		// Play corresponding spawn sound
		// TODO
	}
}
