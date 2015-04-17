using UnityEngine;
using System.Collections;

public class Weapon_Torpedo : Weapon {

	public HomingTorpedo homingTorpedo;
	public GameObject gunMount;
	public float projectileVelocity = 10.0f;
	public float gunOffset = 0.2f;
	public bool inheritVelocity = false;
	
	override public void fire(){
		if(remainingCooldown <= 0f){
			HomingTorpedo newTorpedo = Instantiate (homingTorpedo, gunMount.transform.position + this.transform.up * gunOffset, this.transform.rotation) as HomingTorpedo;
			Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(),newTorpedo.gameObject.GetComponent<Collider2D>());
			newTorpedo.torpedoFaction = this.GetComponent<ShipControl>().faction;
			if(inheritVelocity){
				newTorpedo.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity;
			}
			newTorpedo.GetComponent<Rigidbody2D>().velocity = newTorpedo.GetComponent<Rigidbody2D>().velocity + (Vector2)this.transform.up * projectileVelocity;
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
