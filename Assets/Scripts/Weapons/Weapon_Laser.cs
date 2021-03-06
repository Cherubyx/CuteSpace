﻿using UnityEngine;
using System.Collections;

public class Weapon_Laser : Weapon {

	public LaserProjectile laserProjectile;
	public GameObject gunMount;
	public float projectileVelocity = 10.0f;
	public float gunOffset = 0.2f;
	public bool inheritVelocity = false;
	
	override public void fire(){
		if(remainingCooldown <= 0f){
			LaserProjectile newLaser = Instantiate (laserProjectile, gunMount.transform.position + this.transform.up * gunOffset, this.transform.rotation) as LaserProjectile;
			Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(),newLaser.gameObject.GetComponent<Collider2D>());
			//Ignore collisions on parent as well
			if(GetComponentInParent<Collider2D>() != null){
				Physics2D.IgnoreCollision(GetComponentInParent<Collider2D>(),newLaser.gameObject.GetComponent<Collider2D>());
			}
			if(inheritVelocity){
				newLaser.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity;
			}
			newLaser.GetComponent<Rigidbody2D>().velocity = newLaser.GetComponent<Rigidbody2D>().velocity + (Vector2)this.transform.up * projectileVelocity;
			remainingCooldown = cooldown;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(remainingCooldown > 0f){
			remainingCooldown -= Time.deltaTime;
		}
	}
}
