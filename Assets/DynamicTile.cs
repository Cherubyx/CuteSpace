using UnityEngine;
using System.Collections;

public class DynamicTile : MonoBehaviour {

	//16 tiles of 2.25 x
	//8 tiles of 1.19 y

	float x_cameraDistanceThreshold = 30.0f;
	float y_cameraDistanceThreshold = 30.0f;

	Camera camera;
	// Use this for initialization
	void Start () {
		camera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = this.transform.position;
		if(camera.gameObject.transform.position.x - this.transform.position.x > x_cameraDistanceThreshold){
			pos.x = pos.x + 60f;
		}
		if(this.transform.position.x - camera.gameObject.transform.position.x > x_cameraDistanceThreshold){
			pos.x = pos.x - 60f;
		}

		if(camera.gameObject.transform.position.y - this.transform.position.y > y_cameraDistanceThreshold){
			pos.y = pos.y + 60f;
		}
		if(this.transform.position.y - camera.gameObject.transform.position.y > y_cameraDistanceThreshold){
			pos.y = pos.y - 60f;
		}

		this.transform.position = pos;
	}
}
