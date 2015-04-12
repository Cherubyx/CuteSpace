using UnityEngine;
using System.Collections;

public class ScrollZoom : MonoBehaviour {

	public GameObject focus;
	float targetCameraSize;

	// Use this for initialization
	void Start () {
		targetCameraSize = this.GetComponent<Camera>().orthographicSize;
		focus = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(focus == null){
			focus = GameObject.FindObjectOfType<ShipControl>().gameObject;
		}
		this.GetComponent<Camera>().orthographicSize = Mathf.MoveTowards(this.GetComponent<Camera>().orthographicSize, targetCameraSize, Time.deltaTime * 5f * Mathf.Abs(this.GetComponent<Camera>().orthographicSize - targetCameraSize));
		if(Input.GetAxis("Mouse ScrollWheel") > 0){
			targetCameraSize += -0.5f;
		}
		else if(Input.GetAxis("Mouse ScrollWheel") < 0){
			targetCameraSize -= -0.5f;
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

		Vector3 pos = focus.transform.position;
		pos.z = -10f;
		this.transform.position = pos;
	}
}
