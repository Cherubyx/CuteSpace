using UnityEngine;
using System.Collections;

abstract public class Weapon : MonoBehaviour {

	//Cost for every call to fire
	public float energyCost = 1f;
	//Cooldown between calls to fire
	public float cooldown = 1f;
	//Remaining cooldown on weapon.
	//TODO: Currently public for visibility. Should change to proper get.
	public float remainingCooldown = 0f;

	abstract public void fire();
	virtual public void ceaseFire(){
		//do nothing
	}
}
