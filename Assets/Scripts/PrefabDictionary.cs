using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrefabDictionary : MonoBehaviour {

	Dictionary<string, GameObject> prefabDictionary;

	public GameObject ship_300i;
	public GameObject ship_pirate;
	public GameObject ship_broadsword;

	// Use this for initialization
	void Start () {
		prefabDictionary = new Dictionary<string, GameObject> ();
		prefabDictionary.Add ("300i", ship_300i);
		prefabDictionary.Add ("pirate", ship_pirate);
		prefabDictionary.Add ("broadsword", ship_broadsword);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject getPrefab(string prefabName){
		return prefabDictionary[prefabName];
	}

	public GameObject instantiatePrefab(string prefabName, Vector3 position, Quaternion rotation){
		return Instantiate (prefabDictionary [prefabName], position, rotation) as GameObject;
	}
}
