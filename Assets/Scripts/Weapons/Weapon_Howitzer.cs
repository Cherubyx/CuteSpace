using UnityEngine;
using System.Collections;

public class Weapon_Howitzer : Weapon {

	public GameObject howitzerProjectile;
	public GameObject gunMount;
	public float projectileForce;

	public override void fire(){
		GameObject projectile = GameObject.Instantiate(howitzerProjectile,gunMount.transform.position,gunMount.transform.rotation) as GameObject;
		projectile.GetComponent<Rigidbody2D>().AddForce(projectile.transform.up*projectileForce);
	}
}
