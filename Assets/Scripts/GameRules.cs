using UnityEngine;
using System.Collections;

public class GameRules : MonoBehaviour {

	PrefabDictionary prefabDictionary;
	float spawnTimer;

	// Use this for initialization
	void Start () {
		spawnTimer = 5.0f;
		prefabDictionary = GameObject.Find ("PrefabDictionary").GetComponent<PrefabDictionary>();
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		spawnTimer -= Time.deltaTime;
		if (spawnTimer <= 0f) {
			prefabDictionary.instantiatePrefab ("pirate", new Vector3 (0f, 0f, 0f), Quaternion.identity);
			spawnTimer = 5.0f;
		}

	
	}
}
