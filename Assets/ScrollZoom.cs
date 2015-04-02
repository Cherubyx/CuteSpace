using UnityEngine;
using System.Collections;

public class ScrollZoom : MonoBehaviour {

	public GameObject focus;
	// Use this for initialization
	void Start () {
		focus = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Mouse ScrollWheel") > 0){
			this.GetComponent<Camera>().orthographicSize = this.GetComponent<Camera>().orthographicSize - 0.3f;
		}
		if(Input.GetAxis("Mouse ScrollWheel") < 0){
			this.GetComponent<Camera>().orthographicSize = this.GetComponent<Camera>().orthographicSize + 0.3f;
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
