using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrefabDictionary : MonoBehaviour {

	Dictionary<string, GameObject> prefabDictionary;

	public GameObject ship_shrike;
	public GameObject ship_sparrowhawk;
	public GameObject ship_owlbear;
	public GameObject ship_hornet;
	public GameObject ship_stinger;
	public GameObject ship_crabdragon;
	public GameObject ship_whaleshark;
	public GameObject ship_pelican;
	public GameObject ship_firebat;
	public GameObject ship_heron;
	public GameObject ship_wolfhound;

	public GameObject fleet_flockofherons;

	public GameObject object_jumpgate;
	public GameObject object_spacestation;
	public GameObject object_random_asteroid;
	public GameObject object_random_asteroid_field;

	public GameObject pickup_armor;
	public GameObject pickup_energy;
	public GameObject pickup_dogecoin;
	public GameObject pickup_cheezburger;

	public GameObject effect_warpflash;


	void Awake(){
		if(prefabDictionary == null){
			populatePrefabDictionary();
		}
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void populatePrefabDictionary(){
		prefabDictionary = new Dictionary<string, GameObject> ();
		prefabDictionary.Add ("shrike", ship_shrike);
		prefabDictionary.Add ("sparrowhawk", ship_sparrowhawk);
		prefabDictionary.Add ("owlbear", ship_owlbear);
		prefabDictionary.Add ("hornet", ship_hornet);
		prefabDictionary.Add ("stinger", ship_stinger);
		prefabDictionary.Add ("crabdragon", ship_crabdragon);
		prefabDictionary.Add ("whaleshark", ship_whaleshark);
		prefabDictionary.Add ("pelican", ship_pelican);
		prefabDictionary.Add ("firebat", ship_firebat);
		prefabDictionary.Add ("heron", ship_heron);
		prefabDictionary.Add ("wolfhound", ship_wolfhound);
		
		prefabDictionary.Add ("flockofherons", fleet_flockofherons);
		
		prefabDictionary.Add ("jumpgate", object_jumpgate);
		prefabDictionary.Add ("spacestation", object_spacestation);
		prefabDictionary.Add ("random_asteroid", object_random_asteroid);
		prefabDictionary.Add ("random_asteroid_field", object_random_asteroid_field);
		
		prefabDictionary.Add ("pickup_armor", pickup_armor);
		prefabDictionary.Add ("pickup_energy", pickup_energy);
		prefabDictionary.Add ("pickup_dogecoin", pickup_dogecoin);
		prefabDictionary.Add ("pickup_cheezburger", pickup_cheezburger);
		
		prefabDictionary.Add ("warpflash",effect_warpflash);
	}

	public GameObject getPrefab(string prefabName){
		if(prefabDictionary == null){
			populatePrefabDictionary();
		}
		return prefabDictionary[prefabName];
	}

	public GameObject instantiatePrefab(string prefabName, Vector3 position, Quaternion rotation){
		if(prefabDictionary == null){
			populatePrefabDictionary();
		}
		return Instantiate (prefabDictionary [prefabName], position, rotation) as GameObject;
	}
}
