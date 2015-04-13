using UnityEngine;
using System.Collections;

public class ScrollZoom : MonoBehaviour {

	public GameObject focus;
	float targetCameraSize;
	float maxCameraSize = 22;
	float minCameraSize = 1;

	// Use this for initialization
	void Start () {
		if(this.GetComponent<Camera>().orthographic){
			targetCameraSize = this.GetComponent<Camera>().orthographicSize;
		}
		else{
			targetCameraSize = -this.transform.position.z/2;
		}

		focus = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(focus == null){
			focus = GameObject.FindObjectOfType<ShipControl>().gameObject;
		}
		if(this.GetComponent<Camera>().orthographic){
			this.GetComponent<Camera>().orthographicSize = Mathf.MoveTowards(this.GetComponent<Camera>().orthographicSize, targetCameraSize, Time.deltaTime * 5f * Mathf.Abs(this.GetComponent<Camera>().orthographicSize - targetCameraSize));
		}

		if(Input.GetAxis("Mouse ScrollWheel") > 0){
			targetCameraSize += -0.5f;
		}
		else if(Input.GetAxis("Mouse ScrollWheel") < 0){
			targetCameraSize -= -0.5f;
		}

		targetCameraSize = Mathf.Clamp(targetCameraSize,minCameraSize,maxCameraSize);

		Vector3 pos = focus.transform.position;
		if(this.GetComponent<Camera>().orthographic){
			pos.z = -10f;
		}
		else{
			pos.z = Mathf.MoveTowards(this.transform.position.z, -targetCameraSize*2f, Time.deltaTime*5f);
		}
		this.transform.position = pos;

		if (Input.GetKey (KeyCode.F)) {
			focus = GameObject.Find("Fleet").gameObject;
		}





		/*
		Vector3 cameraPos = this.transform.position;
		Vector3 focusPos = focus.transform.position;
		Vector3 newPos = new Vector3();
		newPos.x = Mathf.Clamp(cameraPos.x,focusPos.x - 5f, focusPos.x + 5f);
		newPos.y = Mathf.Clamp(cameraPos.y,focusPos.y - 5f, focusPos.y + 5f);
		newPos.z = -10;
		this.transform.position = newPos;
		*/


	}
}
