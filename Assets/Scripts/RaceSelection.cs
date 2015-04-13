using UnityEngine;
using System.Collections;

public class RaceSelection : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void selectDog() {
		PersistentGameData.playerRace = "dog";
		PersistentGameData.playerShipName = "hornet";
		PersistentGameData.overworldOriginPosition = new Vector3(-8.28f,4.36f,0f);
		PersistentGameData.overworldDestinationName = "CanisMajor";
		StartCoroutine(WaitAndLoadLevel(0.5f, "Overworld"));
	}

	public void selectCat() {
		PersistentGameData.playerRace = "cat";
		PersistentGameData.playerShipName = "stinger";
		PersistentGameData.overworldOriginPosition = new Vector3(9.28f, 2.36f,0f);
		PersistentGameData.overworldDestinationName = "KittyPrime";
		StartCoroutine(WaitAndLoadLevel(0.5f, "Overworld"));
	}

	IEnumerator WaitAndLoadLevel(float waitTime, string levelName) {
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel(levelName);
	}
}
