using UnityEngine;
using System.Collections;

public class Die : MonoBehaviour {

	public float remainingTime = 3.0f;

	// Update is called once per frame
	void Update () {
		remainingTime -= Time.deltaTime;
		if(remainingTime <= 0f){
			Destroy(this.gameObject);
		}
	}
}
