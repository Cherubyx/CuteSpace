using UnityEngine;
using System.Collections;

public class Weapon_OmniLaser : Weapon {

	public OmniLaser omni;
	public GameObject gunMount;
	float cooldown = 1f;
	float remainingCooldown = 0f;
	float projectileVelocity = 10.0f;
	float gunOffset = 0.2f;
	
	override public void fire(){
		if(remainingCooldown <= 0f){
			OmniLaser newOmni = Instantiate (omni, gunMount.transform.position + this.transform.up * gunOffset, this.transform.rotation) as OmniLaser;
			Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(),newOmni.gameObject.GetComponent<Collider2D>());
			newOmni.GetComponent<Rigidbody2D>().velocity = this.transform.up * projectileVelocity;
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
