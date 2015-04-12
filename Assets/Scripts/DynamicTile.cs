using UnityEngine;
using System.Collections;

public class DynamicTile : MonoBehaviour {

	float x_cameraDistanceThreshold = 50.0f;
	float y_cameraDistanceThreshold = 50.0f;

	Camera camera;
	// Use this for initialization
	void Start () {
		camera = GameObject.Find("Main Camera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = this.transform.position;
		if(camera.gameObject.transform.position.x - this.transform.position.x > x_cameraDistanceThreshold){
			pos.x = pos.x + 100f;
		}
		if(this.transform.position.x - camera.gameObject.transform.position.x > x_cameraDistanceThreshold){
			pos.x = pos.x - 100f;
		}

		if(camera.gameObject.transform.position.y - this.transform.position.y > y_cameraDistanceThreshold){
			pos.y = pos.y + 100f;
		}
		if(this.transform.position.y - camera.gameObject.transform.position.y > y_cameraDistanceThreshold){
			pos.y = pos.y - 100f;
		}

		this.transform.position = pos;
	}
}
