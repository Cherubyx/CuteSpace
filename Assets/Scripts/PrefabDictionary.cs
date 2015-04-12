﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrefabDictionary : MonoBehaviour {

	Dictionary<string, GameObject> prefabDictionary;

	public GameObject ship_shrike;
	public GameObject ship_sparrowhawk;
	public GameObject ship_owlbear;
	public GameObject ship_hornet;
	public GameObject ship_crabdragon;
	public GameObject ship_whaleshark;
	public GameObject ship_pelican;
	public GameObject ship_firebat;
	public GameObject ship_heron;

	public GameObject object_jumpgate;
	public GameObject object_spacestation;

	public GameObject pickup_armor;
	public GameObject pickup_energy;

	public GameObject effect_warpflash;


	void Awake(){
		prefabDictionary = new Dictionary<string, GameObject> ();
		prefabDictionary.Add ("shrike", ship_shrike);
		prefabDictionary.Add ("sparrowhawk", ship_sparrowhawk);
		prefabDictionary.Add ("owlbear", ship_owlbear);
		prefabDictionary.Add ("hornet", ship_hornet);
		prefabDictionary.Add ("crabdragon", ship_crabdragon);
		prefabDictionary.Add ("whaleshark", ship_whaleshark);
		prefabDictionary.Add ("pelican", ship_pelican);
		prefabDictionary.Add ("firebat", ship_firebat);
		prefabDictionary.Add ("heron", ship_heron);

		prefabDictionary.Add ("jumpgate", object_jumpgate);
		prefabDictionary.Add ("spacestation", object_spacestation);

		prefabDictionary.Add ("pickup_armor", pickup_armor);
		prefabDictionary.Add ("pickup_energy", pickup_energy);

		prefabDictionary.Add ("warpflash",effect_warpflash);
	}

	// Use this for initialization
	void Start () {

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
