using UnityEngine;
using System.Collections;

public class MapMeteor : MonoBehaviour {
	public float rotationSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(0f,0f,Time.deltaTime * rotationSpeed);
	}
}
