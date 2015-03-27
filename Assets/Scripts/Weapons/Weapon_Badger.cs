using UnityEngine;
using System.Collections;

public class Weapon_Badger : Weapon {

	public BadgerLaser badger;
	public GameObject gunMount;
	float cooldown = 0.20f;
	float remainingCooldown = 0f;
	float projectileVelocity = 10.0f;
	float gunOffset = 0.2f;

	override public void fire(){
		if(remainingCooldown <= 0f){
			BadgerLaser newBadger = Instantiate (badger, gunMount.transform.position + this.transform.up * gunOffset, this.transform.rotation) as BadgerLaser;
			Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(),newBadger.gameObject.GetComponent<Collider2D>());
			newBadger.GetComponent<Rigidbody2D>().velocity = this.transform.up * projectileVelocity;
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
