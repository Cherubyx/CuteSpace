using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_ShipControl : MonoBehaviour {

	//Attack behaviour parameters
	public float attack_minimumDistance = 4.0f;
	public float attack_firingDistance = 10.0f;
	public float attack_forwardThrustTargetAngle = 45f;
	public float attack_firingArc = 20f;
	protected float projectileSpeed = 10f;

	//Wander behaviour parameters
	public float wander_closingDistance = 1.0f;
	public float wander_forwardThrustTargetAngle = 45f;

	//Flee behaviour parameters
	public float flee_forwardThrustTargetAngle = 30f;

	//Targeting variables
	public Vector2 orientationTarget;
	public List<ShipControl> targetShipList;
	public List<ShipControl> fleeShipList;
	public Stack<GameObject> targetStack;
	protected ShipControl bestAttackTarget;


	//Reference to the ship controls
	protected ShipControl shipControl;

	//The AI will ignore entities beyond this radius
	public float awarenessRadius = 20f;

	protected List<ShipControl> getNearbyEnemies(){
		List<ShipControl> shipList = new List<ShipControl>();
		Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(this.transform.position,awarenessRadius);
		foreach(Collider2D nearbyObject in nearbyObjects){
			if(nearbyObject.gameObject != this.gameObject && nearbyObject.GetComponent<ShipControl>() != null && isEnemy (nearbyObject.GetComponent<ShipControl>())){
				shipList.Add(nearbyObject.GetComponent<ShipControl>());
			}
		}
		return shipList;
	}

	//Return the best candidate to attack from the given list
	protected ShipControl getBestTarget(List<ShipControl> targetList){
		float maxVal = 0f;
		float curVal = 0f;
		if(targetList.Count <= 0){
			return null;
		}
		ShipControl bestTarget = targetList[0];
		foreach(ShipControl potentialTarget in targetList){
			curVal = evaluateTarget(potentialTarget);
			if(curVal > maxVal){
				bestTarget = potentialTarget;
				maxVal = curVal;
			}
		}
		return bestTarget;
	}
	
	//Evaluate a ship to see whether we should attack it. Returns value between 0 and 1.
	protected float evaluateTarget(ShipControl targetShip){
		float value = 0f;
		//Add 0.1 to 0.5 depending on proximity of ship
		value += (0.5f - 0.4f * Vector2.Distance(this.transform.position,targetShip.transform.position)/awarenessRadius); 
		//Add 0.1 to 0.5 depending on health of ship (prioritize picking off weaker ships by default)
		value += (0.5f - 0.4f * targetShip.HP / targetShip.maxHP);
		return value;
	}

	protected bool isEnemy(ShipControl ship){
		return PersistentGameData.factionEnemies[(int)shipControl.faction,(int)ship.faction];
	}



	protected void attack_updateRotation(){
		if(bestAttackTarget != null){
			orientationTarget = (Vector2)bestAttackTarget.transform.position + bestAttackTarget.GetComponent<Rigidbody2D>().velocity * Vector2.Distance(this.transform.position,(Vector2)bestAttackTarget.transform.position) / projectileSpeed;
			shipControl.updateRotation(orientationTarget);
		}
	}
	
	protected void attack_updateThrust(){
		if(bestAttackTarget != null && Vector2.Distance (this.transform.position, bestAttackTarget.transform.position) > attack_firingDistance && Mathf.Abs (MathHelper.getRemainingAngleToTarget(this.gameObject,bestAttackTarget.transform.position)) < attack_forwardThrustTargetAngle) {
			shipControl.activateMainEngines ();
		}
		else {
			shipControl.cutMainEngines();
		}	
	}
	
	protected void attack_updateFire(){
		if(bestAttackTarget != null && Vector2.Distance(this.transform.position,orientationTarget) < attack_firingDistance && Mathf.Abs (MathHelper.getRemainingAngleToTarget(this.gameObject,orientationTarget)) < attack_firingArc){
			shipControl.firePrimaryWeapons();
			shipControl.fireSecondaryWeapons();
		}
		else{
			shipControl.ceaseFirePrimaryWeapons();
			shipControl.ceaseFireSecondaryWeapons();
		}
	}

	protected void wander_updateRotation(){
		orientationTarget = orientationTarget + new Vector2(Random.Range(-5f,5f) * Time.deltaTime,Random.Range(-5f,5f) * Time.deltaTime);
		shipControl.updateRotation(orientationTarget);
	}
	
	protected void wander_updateThrust(){
		if(Vector2.Distance (this.transform.position, orientationTarget) > wander_closingDistance && Mathf.Abs (MathHelper.getRemainingAngleToTarget(this.gameObject,orientationTarget)) < wander_forwardThrustTargetAngle) {
			shipControl.activateMainEngines ();
		}
		else {
			shipControl.cutMainEngines();
		}	
	}
	
	protected void flee_updateRotation(){
		if(fleeShipList.Count > 0){
			//Find the center of the enemy formation
			Vector2 enemyBarycenter = Vector2.zero;
			foreach(ShipControl fleeTarget in fleeShipList){
				if(fleeTarget != null){
					enemyBarycenter += (Vector2)fleeTarget.transform.position;
				}
			}
			enemyBarycenter = enemyBarycenter / fleeShipList.Count;
			
			//Orient ourselves to face away from it
			Vector2 vectorToTarget = enemyBarycenter - (Vector2)this.transform.position;
			orientationTarget = (Vector2)this.transform.position - vectorToTarget;
			shipControl.updateRotation(orientationTarget);
		}		
	}
	
	protected void flee_updateThrust(){
		if(Mathf.Abs (MathHelper.getRemainingAngleToTarget(this.gameObject,orientationTarget)) < flee_forwardThrustTargetAngle) {
			shipControl.activateMainEngines ();
		}
		else {
			shipControl.cutMainEngines();
		}	
	}
	
	
	protected void OnDrawGizmos() {
		Gizmos.DrawWireSphere(orientationTarget, 0.5f);
	}
}


