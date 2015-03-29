using UnityEngine;
using System.Collections;

public class Weapon_ButtMissile : Weapon {

	public ButtMissile missileProjectile;
	public GameObject gunMount;
	float cooldown = 1.0f;
	float remainingCooldown = 0f;
	
	override public void fire(){
		if(remainingCooldown <= 0f){
			string targetTeamName = this.gameObject.tag == "Team1" ? "Team2" : "Team1";
			ButtMissile newMissile = Instantiate (missileProjectile, gunMount.transform.position, Quaternion.Euler((this.gameObject.transform.rotation.eulerAngles + new Vector3(0,0,180f)))) as ButtMissile;
			Physics2D.IgnoreCollision(this.gameObject.GetComponent<Collider2D>(),newMissile.gameObject.GetComponent<Collider2D>());
			newMissile.setTargetTeam (targetTeamName);
			//newMissile.GetComponent<Rigidbody2D>().velocity = this.GetComponent<Rigidbody2D>().velocity;
			remainingCooldown = cooldown;
		}	
	}

	void Update () {
		if(remainingCooldown > 0f){
			remainingCooldown -= Time.deltaTime;
		}
	}
}
