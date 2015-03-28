using UnityEngine;
using System.Collections;

abstract public class Weapon : MonoBehaviour {

	public float energyCost = 1f;
	public float cooldown = 1f;
	public float remainingCooldown = 0f;

	abstract public void fire();
}
