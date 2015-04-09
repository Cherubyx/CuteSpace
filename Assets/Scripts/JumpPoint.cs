﻿using UnityEngine;
using System.Collections;

public class JumpPoint : MonoBehaviour {

	public string exitSystemName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		PersistentGameData.overworldDestinationName = exitSystemName;
		StartCoroutine(WaitFor(2.0f));
		Application.LoadLevel("Overworld");
	}

	IEnumerator WaitFor(float waitTime) {
		yield return new WaitForSeconds(waitTime);
	}
}
