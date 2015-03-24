using UnityEngine;
using System.Collections;

public class Weapon_Cruiser_Missile : Weapon {

	public LargeHomingMissile missileProjectile;
	public GameObject gunMount;
	public float projectileForce;

	override public void fire(){
		string targetTeamName = this.gameObject.tag == "Team1" ? "Team2" : "Team1";
		LargeHomingMissile newMissile = Instantiate (missileProjectile, gunMount.transform.position, this.transform.rotation) as LargeHomingMissile;
		newMissile.setTargetTeam (targetTeamName);
	}
}
