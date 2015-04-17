using UnityEngine;
using System.Collections;

public class LaserTurret : MonoBehaviour {

	ShipControl ship;
	GameObject target;
	public float turnSpeed = 1.5f;
	public Weapon_Laser laser1;
	public Weapon_Laser laser2;
	public float attack_firingDistance = 10.0f;
	public float attack_firingArc = 20f;

	// Use this for initialization
	void Start () {
		ship = GetComponentInParent<ShipControl>();
	}
	
	// Update is called once per frame
	void Update () {
		ShipControl[] nearbyShips = GameObject.FindObjectsOfType<ShipControl>();
		target = null;
		float shortestDistance = float.MaxValue;
		
		foreach(ShipControl potentialTarget in nearbyShips){
			if(isEnemy(potentialTarget) && Vector2.Distance(this.transform.position,potentialTarget.transform.position) < shortestDistance){
				target = potentialTarget.gameObject;
				shortestDistance = Vector2.Distance(this.transform.position,potentialTarget.transform.position);
			}
		}

		UpdateRotation();
		UpdateFire();
	}

	void UpdateRotation (){
		//Make the turret turn to face the target
		if(target != null){
			float targetAngle = MathHelper.getAngleToTarget(this.transform.position,target.transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler (0, 0, targetAngle), turnSpeed * Time.deltaTime);	
		}

	}

	protected void UpdateFire(){
		if(target != null && Vector2.Distance(this.transform.position,target.transform.position) < attack_firingDistance && Mathf.Abs (MathHelper.getRemainingAngleToTarget(this.gameObject,target.transform.position)) < attack_firingArc && ship.energy >= (laser1.energyCost + laser2.energyCost) && laser1.remainingCooldown <= 0f && laser2.remainingCooldown <= 0f){
			laser1.fire();
			laser2.fire();
			ship.energy -= (laser1.energyCost + laser2.energyCost);
		}
		else{
			laser1.ceaseFire();
			laser2.ceaseFire();
		}
	}

	protected bool isEnemy(ShipControl targetShip){
		return PersistentGameData.factionEnemies[(int)ship.faction,(int)targetShip.faction];
	}
}
