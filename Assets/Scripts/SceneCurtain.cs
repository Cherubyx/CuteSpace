using UnityEngine;
using System.Collections;

public class SceneCurtain : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<SpriteRenderer>().color = new Color(0.0f,0.0f,0.0f,1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<SpriteRenderer>().color = new Color(0.0f,0.0f,0.0f,this.GetComponent<SpriteRenderer>().color.a - Time.deltaTime/2f);
	}
}
