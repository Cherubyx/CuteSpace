﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraControl : MonoBehaviour {

	public GameObject player;
	public List<Vector2> focusObjectsList;
	public float cameraMoveSpeed = 1f;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {

		//Move camera to center point between all focus objects
		populateFocusObjectsList();
		Vector2 cameraFocus = getCameraFocusPosition();
		this.transform.position = Vector2.MoveTowards (this.transform.position, cameraFocus, cameraMoveSpeed);

		//Reset camera z position to default
		Vector3 pos = transform.position;
		pos.z = -10;
		transform.position = pos;

		//Adjust camera zoom based on distance between furthest focus object and camera focus point
		camera.orthographicSize = Mathf.Max(4f,getLargestDistanceFromFocusPoint());
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
		foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Player")) {
			focusObjectsList.Add(gameObject.transform.position);
		}
		foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Enemy")) {
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
