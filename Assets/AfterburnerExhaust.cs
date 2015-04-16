using UnityEngine;
using System.Collections;

public class AfterburnerExhaust : MonoBehaviour {

	public GameObject explosion;
	public float damage = 2f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Color newColor = this.GetComponent<SpriteRenderer>().color;
		newColor.a -= Time.deltaTime/2f;
		if(newColor.a <= 0f){
			Destroy (this.gameObject);
		}
		else{
			this.GetComponent<SpriteRenderer>().color = newColor;
			this.transform.localScale -= (new Vector3(1f,1f,1f)*Time.deltaTime/8f);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		ShipControl ship = other.gameObject.GetComponent<ShipControl>();
		if(ship!=null){						         
			ship.takeDamage(damage);
			Instantiate(explosion,this.transform.position,Quaternion.identity);
			Destroy (this.gameObject);
		}
	}
}
