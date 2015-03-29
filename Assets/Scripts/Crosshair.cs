using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pz.z = 0;
		gameObject.transform.position = pz; 
	}
}
