using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {

	int rewardCoins;
	// Use this for initialization
	void Start () {
		foreach(ObjectLocation objectLocation in PersistentGameData.objectSpawnList){
			Instantiate(objectLocation.gameObject,objectLocation.position,objectLocation.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
