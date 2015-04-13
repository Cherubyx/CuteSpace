using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_StateMachine : AI_ShipControl {

	//Assign the behaviour to run to a delegate that is called every update
	delegate void AI_behaviour();
	AI_behaviour delegatedBehaviour;

	//Strafe parameters
	public float strafe_maximumDistance = 8.0f;
	public float strafe_avoidanceArc = 90f;
	public float strafe_minimumVelocity = 2.0f;
	
	// Use this for initialization
	void Start () {
		/*
		targetStack = new Stack<GameObject>();
		
		//TODO: remove test code
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Team2");
		foreach(GameObject target in enemies){
			targetStack.Push (target);
		}

		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach(GameObject target in players){
			targetStack.Push (target);
		}
		*/
		
		delegatedBehaviour = attack;
		orientationTarget = new Vector2(0f,0f);
		shipControl = this.gameObject.GetComponent<ShipControl>();
	}
	
	// Update is called once per frame
	void Update () {
		targetShipList = getNearbyEnemies();
		bestAttackTarget = getBestTarget(targetShipList);
		//If we don't have a target, check the target stack!
		//if(bestAttackTarget == null && targetStack.Count > 0){
		//	bestAttackTarget = targetStack.Pop();
		//}
		
		delegatedBehaviour();
	}
	
	//Wander Behaviour
	private void wander(){
		//State transitions
		//If we have a target, attack it
		if( bestAttackTarget != null ){
			delegatedBehaviour = attack;
		}
		wander_updateRotation();
		wander_updateThrust();
	}
	
	void wander_updateRotation(){
		orientationTarget = orientationTarget + new Vector2(Random.Range(-5f,5f) * Time.deltaTime,Random.Range(-5f,5f) * Time.deltaTime);
		shipControl.updateRotation(orientationTarget);
	}
	
	void wander_updateThrust(){
		if(Vector2.Distance (this.transform.position, orientationTarget) > wander_closingDistance && Mathf.Abs (MathHelper.getRemainingAngleToTarget(this.gameObject,orientationTarget)) < wander_forwardThrustTargetAngle) {
			shipControl.activateMainEngines ();
		}
		else {
			shipControl.cutMainEngines();
		}	
	}
	
	//Flee Behaviour
	private void flee(){
		//State transitions
		//If energy is full, get back into the fight
		if(shipControl.energy == shipControl.maxEnergy){
			delegatedBehaviour = attack;
		}

		//Get all ships in range
		Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(this.transform.position,awarenessRadius);
		fleeShipList.Clear();
		foreach(Collider2D nearbyObject in nearbyObjects){
			if(nearbyObject.gameObject != this.gameObject && nearbyObject.GetComponent<ShipControl>() != null && nearbyObject.tag != this.tag){
				fleeShipList.Add(nearbyObject.GetComponent<ShipControl>());
			}
		}
		
		//State behaviours
		flee_updateRotation();
		flee_updateThrust();
	}
	
	void flee_updateRotation(){
		if(bestAttackTarget != null){
			Vector2 vectorToTarget = bestAttackTarget.transform.position - this.transform.position;
			orientationTarget = (Vector2)this.transform.position - vectorToTarget;
			shipControl.updateRotation(orientationTarget);
		}
	}
	
	void flee_updateThrust(){
		if(Mathf.Abs (MathHelper.getRemainingAngleToTarget(this.gameObject,orientationTarget)) < flee_forwardThrustTargetAngle) {
			shipControl.activateMainEngines ();
		}
		else {
			shipControl.cutMainEngines();
		}	
	}
	
	//Attack Behaviour
	private void attack(){
		//State transitions
		//If energy is low (below 10%), flee
		if( bestAttackTarget == null ){
			delegatedBehaviour = wander;
			shipControl.ceaseFirePrimaryWeapons();
			return;
		}
		else if(shipControl.energy < shipControl.maxEnergy / 10f){
			delegatedBehaviour = flee;
			shipControl.ceaseFirePrimaryWeapons();
		}
		else if(Vector2.Distance (this.transform.position, bestAttackTarget.transform.position) < attack_minimumDistance){
			delegatedBehaviour = strafe;
		}
		
		//State behaviours
		attack_updateRotation();
		attack_updateThrust();
		attack_updateFire();
		
	}
	
	void attack_updateRotation(){
		if(bestAttackTarget != null){
			orientationTarget = (Vector2)bestAttackTarget.transform.position + bestAttackTarget.GetComponent<Rigidbody2D>().velocity * Vector2.Distance(this.transform.position,orientationTarget) / projectileSpeed;
			shipControl.updateRotation(orientationTarget);
		}
	}
	
	void attack_updateThrust(){
		if(bestAttackTarget != null && Vector2.Distance (this.transform.position, bestAttackTarget.transform.position) > attack_firingDistance && Mathf.Abs (MathHelper.getRemainingAngleToTarget(this.gameObject,bestAttackTarget.transform.position)) < attack_forwardThrustTargetAngle) {
			shipControl.activateMainEngines ();
		}
		else {
			shipControl.cutMainEngines();
		}	
	}
	
	void attack_updateFire(){
		if(bestAttackTarget != null && Vector2.Distance(this.transform.position,orientationTarget) < attack_firingDistance && Mathf.Abs (MathHelper.getRemainingAngleToTarget(this.gameObject,orientationTarget)) < attack_firingArc){
			shipControl.firePrimaryWeapons();
			shipControl.fireSecondaryWeapons();
		}
		else{
			shipControl.ceaseFirePrimaryWeapons();
			shipControl.ceaseFireSecondaryWeapons();
		}
	}

	//Strafe behaviour	
	private void strafe(){
		//State transitions
		if( bestAttackTarget == null ){
			delegatedBehaviour = wander;
			return;
		}
		else if(Vector2.Distance (this.transform.position, bestAttackTarget.transform.position) > strafe_maximumDistance){
			delegatedBehaviour = attack;
		}
		
		//State behaviours
		Vector2 vectorToTarget = bestAttackTarget.transform.position - this.transform.position;
		Vector2 velocity = this.gameObject.GetComponent<Rigidbody2D>().velocity;

		attack_updateRotation();
		attack_updateFire();

		/*
		if(Mathf.Abs(Vector2.Angle(vectorToTarget,velocity)) < strafe_avoidanceArc || velocity.magnitude < strafe_minimumVelocity){
			flee_updateRotation();
			flee_updateThrust();
		}
		else{
			attack_updateRotation();
			attack_updateFire();
		}
		*/
	}

	protected void OnDrawGizmos() {
		Gizmos.DrawWireSphere(orientationTarget, 0.5f);
	}
}

