using UnityEngine;
using System.Collections;

public class SceneCurtain : MonoBehaviour {

	public bool isOpening;

	// Use this for initialization
	void Start () {
		isOpening = true;
		this.GetComponent<SpriteRenderer>().color = new Color(0.0f,0.0f,0.0f,1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(isOpening){
			this.GetComponent<SpriteRenderer>().color = new Color(0.0f,0.0f,0.0f,this.GetComponent<SpriteRenderer>().color.a - Time.deltaTime/2f);
		}
		else{
			this.GetComponent<SpriteRenderer>().color = new Color(0.0f,0.0f,0.0f,this.GetComponent<SpriteRenderer>().color.a + Time.deltaTime/2f);
		}
	}

	public void openCurtain(){
		if(this.GetComponent<SpriteRenderer>().color.a > 1f){
			this.GetComponent<SpriteRenderer>().color = new Color(0f,0f,0f,1f);
		}
		isOpening = true;
	}

	public void closeCurtain(){
		if(this.GetComponent<SpriteRenderer>().color.a < 0f){
			this.GetComponent<SpriteRenderer>().color = new Color(0f,0f,0f,0f);
		}
		isOpening = false;
	}
}
