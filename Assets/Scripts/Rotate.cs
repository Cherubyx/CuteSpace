using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public float rotationRate = 180f;
	public bool random;

	// Use this for initialization
	void Start () {
		if(random){
			rotationRate = Random.Range(0f,rotationRate);
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(0f,0f,Time.deltaTime * rotationRate);
	}
}
