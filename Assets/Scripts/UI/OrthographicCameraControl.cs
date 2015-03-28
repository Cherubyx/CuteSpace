using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrthographicCameraControl : MonoBehaviour {

	public List<Vector2> focusObjectsList;
	public float heightModifier = 1f;
	public float cameraMoveSpeed = 1f;
	public float cameraZoomSpeed = 1f;
	public float minimumCameraSize = 10f;
	public float maximumCameraSize = 15f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		//Move camera to center point between all focus objects
		populateFocusObjectsList();
		Vector2 cameraFocus = getCameraFocusPosition();
		this.transform.position = Vector2.MoveTowards (this.transform.position, cameraFocus, cameraMoveSpeed * Time.deltaTime);

		//Reset camera z position to default
		//Vector3 pos = transform.position;
		//pos.z = - Mathf.Max(10f,getLargestDistanceFromFocusPoint()*heightModifier);
		//transform.position = pos;
		Vector3 pos = this.transform.position;
		pos.z = -10;
		this.transform.position = pos;

		//Adjust camera zoom based on distance between furthest focus object and camera focus point
		GetComponent<Camera>().orthographicSize = Mathf.MoveTowards(GetComponent<Camera>().orthographicSize,Mathf.Clamp(getLargestDistanceFromFocusPoint(),minimumCameraSize,maximumCameraSize),cameraZoomSpeed * Time.deltaTime);
	}

	Vector2 getCameraFocusPosition(){
		Vector2 cameraFocusPosition = new Vector2();

		foreach (Vector2 objectPosition in focusObjectsList) {
			cameraFocusPosition += objectPosition;
		}
		cameraFocusPosition = cameraFocusPosition / focusObjectsList.Count;
		return cameraFocusPosition;
	}

	void populateFocusObjectsList(){
		focusObjectsList.Clear();
		foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Team1")) {
			focusObjectsList.Add(gameObject.transform.position);
		}
		foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Team2")) {
			focusObjectsList.Add(gameObject.transform.position);
		}
	}

	float getLargestDistanceFromFocusPoint(){
		float distance = 0f;
		float greatestDistance = 0f;
		foreach (Vector2 objectPosition in focusObjectsList) {
			distance = (objectPosition - getCameraFocusPosition()).magnitude;

			if(distance > greatestDistance){
				greatestDistance = distance;			
			}
		}
		return greatestDistance;
	}
}
