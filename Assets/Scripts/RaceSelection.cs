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
		StartCoroutine(WaitAndLoadLevel(0.5f, "Overworld"));
	}

	public void selectCat() {
		PersistentGameData.playerRace = "cat";	
		StartCoroutine(WaitAndLoadLevel(0.5f, "Overworld"));
	}

	IEnumerator WaitAndLoadLevel(float waitTime, string levelName) {
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel(levelName);
	}
}
