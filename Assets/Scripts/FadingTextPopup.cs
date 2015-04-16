using UnityEngine;
using System.Collections;

public class FadingTextPopup : MonoBehaviour {

	bool isIncreasing = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Color color = this.GetComponent<TextMesh> ().color;
		if (color.a >= 1.0f) {
			isIncreasing = false;
		} else if (color.a <= 0.0f) {
			Destroy (this.gameObject);
		}
		color.a += isIncreasing ? Time.deltaTime * 2 : -Time.deltaTime/2;
		this.GetComponent<TextMesh> ().color = color;
	}
}
