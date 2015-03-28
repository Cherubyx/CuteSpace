using UnityEngine;
using System.Collections;

public class SpriteExplosion : MonoBehaviour {

	public float lifeTime = 1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	void Update () {
		lifeTime -= Time.deltaTime;
		if(lifeTime <= 0.0f){
				Destroy(this.gameObject);
	
		}
	}
}
