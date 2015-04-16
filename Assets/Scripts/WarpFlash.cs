using UnityEngine;
using System.Collections;

public class WarpFlash : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.GetComponent<SpriteRenderer>().color = new Color(0.5f,0.5f,0.5f,0.8f);
	}
	
	// Update is called once per frame
	void Update () {
		Color newColor = this.GetComponent<SpriteRenderer>().color;
		newColor.r = Mathf.Clamp( newColor.r + Random.Range(-1f,1f) * Time.deltaTime,0f,1f);
		newColor.g = Mathf.Clamp( newColor.g + Random.Range(-1f,1f) * Time.deltaTime,0f,1f);
		newColor.b = Mathf.Clamp( newColor.b + Random.Range(-1f,1f) * Time.deltaTime,0f,1f);
		//newColor.a += Time.deltaTime / 4f;
		this.transform.localScale = this.transform.localScale + new Vector3(5f,5f,5f) * Time.deltaTime;
		this.GetComponent<SpriteRenderer>().color = newColor;
	}
}
