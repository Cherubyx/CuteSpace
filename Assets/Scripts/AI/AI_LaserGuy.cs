using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_LaserGuy : MonoBehaviour {

	delegate void AI_behaviour();
	AI_behaviour delegatedBehaviour;
	
	public Vector2 orientationTarget;
	public GameObject targetGameObject;
	public Stack<GameObject> targetStack;
	
	//Wander parameters
	private float wander_closingDistance = 1.0f;
	private float wander_forwardThrustTargetAngle = 45f;
	
	//Flee parameters
	private float flee_forwardThrustTargetAngle = 30f;
	
	//Attack parameters
	private float attack_minimumDistance = 2.0f;
	private float attack_firingDistance = 4.0f;
	private float attack_forwardThrustTargetAngle = 15f;
	private float attack_firingArc = 10f;

	//TODO: Replace strafe with some cool new behaviour
	//Strafe parameters
	private float strafe_maximumDistance = 8.0f;
	private float strafe_avoidanceArc = 90f;
	private float strafe_minimumVelocity = 2.0f;
	
	
	private ShipControl shipControl;
	
	// Use this for initialization
	void Start () {
		targetStack = new Stack<GameObject>();

		//TODO: remove test code
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Team2");
		foreach(GameObject target in enemies){
			targetStack.Push (target);
		}

		delegatedBehaviour = attack;
		orientationTarget = new Vector2(0f,0f);
		shipControl = this.gameObject.GetComponent<ShipControl>();
	}
	
	// Update is called once per frame
	void Update () {
		//If we don't have a target, check the target stack!
		if(targetGameObject == null && targetStack.Peek() != null){
			targetGameObject = targetStack.Pop();
		}
		
		delegatedBehaviour();
	}
	
	//Wander Behaviour
	private void wander(){
		//State transitions
		//If we have a target, attack it
		if( targetGameObject != null ){
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
		if( targetGameObject == null ){
			delegatedBehaviour = wander;
			return;
		}
		else if(shipControl.energy == shipControl.maxEnergy){
			delegatedBehaviour = attack;
		}
		
		//State behaviours
		flee_updateRotation();
		flee_updateThrust();
	}
	
	void flee_updateRotation(){
		if(targetGameObject != null){
			Vector2 vectorToTarget = targetGameObject.transform.position - this.transform.position;
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
		if( targetGameObject == null ){
			delegatedBehaviour = wander;
			shipControl.ceaseFirePrimaryWeapons();
			return;
		}
		else if(shipControl.energy < shipControl.maxEnergy / 10f){
			delegatedBehaviour = flee;
			shipControl.ceaseFirePrimaryWeapons();
		}
		else if(Vector2.Distance (this.transform.position, targetGameObject.transform.position) < attack_minimumDistance){
			delegatedBehaviour = strafe;
		}
		
		//State behaviours
		attack_updateRotation();
		attack_updateThrust();
		attack_updateFire();
		
	}
	
	void attack_updateRotation(){
		if(targetGameObject != null){
			orientationTarget = targetGameObject.transform.position;
			shipControl.updateRotation(orientationTarget);
		}
	}
	
	void attack_updateThrust(){
		if(targetGameObject != null && Vector2.Distance (this.transform.position, targetGameObject.transform.position) > attack_firingDistance && Mathf.Abs (MathHelper.getRemainingAngleToTarget(this.gameObject,targetGameObject.transform.position)) < attack_forwardThrustTargetAngle) {
			shipControl.activateMainEngines ();
		}
		else {
			shipControl.cutMainEngines();
		}	
	}
	
	void attack_updateFire(){
		if(targetGameObject != null && Vector2.Distance(this.transform.position,targetGameObject.transform.position) < attack_firingDistance && Mathf.Abs (MathHelper.getRemainingAngleToTarget(this.gameObject,targetGameObject.transform.position)) < attack_firingArc){
			shipControl.firePrimaryWeapons();
		}
		else{
			shipControl.ceaseFirePrimaryWeapons();
		}
	}

	//TODO: Replace strafe with some appropriate laser-y behaviour
	//Strafe behaviour	
	private void strafe(){
		//State transitions
		if( targetGameObject == null ){
			delegatedBehaviour = wander;
			return;
		}
		else if(Vector2.Distance (this.transform.position, targetGameObject.transform.position) > strafe_maximumDistance){
			delegatedBehaviour = attack;
		}
		
		//State behaviours
		Vector2 vectorToTarget = targetGameObject.transform.position - this.transform.position;
		Vector2 velocity = this.gameObject.GetComponent<Rigidbody2D>().velocity;
		
		if(Mathf.Abs(Vector2.Angle(vectorToTarget,velocity)) < strafe_avoidanceArc || velocity.magnitude < strafe_minimumVelocity){
			flee_updateRotation();
			flee_updateThrust();
		}
		else{
			attack_updateRotation();
			attack_updateFire();
		}
	}
}
