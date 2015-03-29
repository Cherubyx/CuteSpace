﻿using UnityEngine;
using System.Collections;

public class LaserProjectile : MonoBehaviour {

	public float lifeTime = 1.25f;
	public float damage = 3f;
	public GameObject explosion;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		updateLife ();
	}
	
	void updateLife(){
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0f) {
			die ();
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		ShipControl ship = other.gameObject.GetComponent<ShipControl>();
		if(ship!=null){
			ship.takeDamage(damage);
			Instantiate(explosion,this.transform.position,Quaternion.identity);
			die ();
		}

	}
	
	void die(){
		Destroy (this.gameObject);
	}
}
