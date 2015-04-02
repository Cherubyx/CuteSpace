using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public float rotationRate = 180f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(0f,0f,Time.deltaTime * rotationRate);
	}
}
