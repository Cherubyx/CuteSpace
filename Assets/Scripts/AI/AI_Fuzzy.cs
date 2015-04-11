using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AI_Fuzzy : AI_ShipControl {

	//Every half second, run the membership functions to determine which action to pursue
	float membershipInterval = 0.5f;
	float membershipTimer;

	//Assign the behaviour to run to a delegate that is called every update
	delegate void AI_behaviour();
	AI_behaviour delegatedBehaviour;

	//The AI will ignore entities beyond this radius
	public float awarenessRadius = 15f;

	//Targeting variables
	public Vector2 orientationTarget;
	public List<ShipControl> targetShipList;
	public List<ShipControl> fleeShipList;
	public Stack<GameObject> targetStack;
	
	//Wander parameters
	private float wander_closingDistance = 1.0f;
	private float wander_forwardThrustTargetAngle = 45f;
	
	//Flee parameters
	private float flee_forwardThrustTargetAngle = 30f;
	private float bravery_quotient = 2.0f;
	private float energy_evaluation_coefficient = 2.0f;
	
	//Attack parameters
	private ShipControl bestAttackTarget;
	private float attack_minimumDistance = 4.0f;
	private float attack_firingDistance = 10.0f;
	private float attack_forwardThrustTargetAngle = 45f;
	private float attack_firingArc = 20f;
	private float projectileSpeed = 10f;

	//Reference to the ship controls
	private ShipControl shipControl;

	// Use this for initialization
	void Start () {

		membershipTimer = 0f;
		delegatedBehaviour = attack;
		orientationTarget = new Vector2(0f,0f);
		shipControl = this.gameObject.GetComponent<ShipControl>();
	
	}
	
	// Update is called once per frame
	void Update () {

		//Every interval, run the membership functions decide on a behaviour
		membershipTimer -= Time.deltaTime;
		if(membershipTimer <= 0){
			evaluateMembershipFunctions();
			membershipTimer = membershipInterval;
		}

		//Run the chosen behaviour
		delegatedBehaviour();
	}

	//Determine the values of the membership functions, then assign the behaviour corresponding to the highest value to the delegatedBehaviour slot.
	void evaluateMembershipFunctions(){
		//TODO: Reimplement with list of functions?

		float mfMax = mf_attack();
		float mfVal = 0f;
		delegatedBehaviour = attack;
		
		mfVal = mf_flee();
		if(mfVal > mfMax){
			delegatedBehaviour = flee;
			mfMax = mfVal;
		}
		
		mfVal = mf_wander();
		if(mfVal > mfMax){
			delegatedBehaviour = wander;
			mfMax = mfVal;
		}

		//TODO: Add seek for pickups
	}

	//Membership function for behaviour "attack"
	float mf_attack(){
		//Get all ships in range
		Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(this.transform.position,awarenessRadius);
		targetShipList.Clear();
		foreach(Collider2D nearbyObject in nearbyObjects){
			if(nearbyObject.gameObject != this.gameObject && nearbyObject.GetComponent<ShipControl>() != null && nearbyObject.tag != this.tag){
				targetShipList.Add(nearbyObject.GetComponent<ShipControl>());
			}
		}
		//If no ships are in range, there is no reason to attack
		if(targetShipList.Count <= 0)
		{
			return 0f;
		}
		//Evaluate them as candidates to attack and return the score of the best candidate
		//TODO: Do we factor in energy as well?
		bestAttackTarget = getBestTarget(targetShipList);
		return(evaluateTarget(bestAttackTarget));
	}

	//Membership function for behaviour "flee"
	float mf_flee(){
		//Get all ships in range
		Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(this.transform.position,awarenessRadius);
		fleeShipList.Clear();
		foreach(Collider2D nearbyObject in nearbyObjects){
			if(nearbyObject.gameObject != this.gameObject && nearbyObject.GetComponent<ShipControl>() != null && nearbyObject.tag != this.tag){
				fleeShipList.Add(nearbyObject.GetComponent<ShipControl>());
			}
		}
		//If no ships are in range, there is no reason to flee
		if(fleeShipList.Count <= 0)
		{
			return 0f;
		}

		//If our health is low, we may want to flee
		float healthFleeValue = (1 - (shipControl.HP/shipControl.maxHP)) / bravery_quotient;

		//If our energy is low, we may want to flee
		//Square the percentage being subtracted to add more weight to fleeing when energy is low
		float energyFleeValue = 1 - Mathf.Pow((shipControl.energy/shipControl.maxEnergy),2);

		//Return the greater of the two values
		return Mathf.Max(healthFleeValue,energyFleeValue);
	}

	//Membership function for behaviour "wander"
	float mf_wander(){
		//We don't really want to wander if we have anything better to do
		return 0.1f;
	}

	//TODO: Implement seeking health pickup
	float mf_seekHealth(){
		return 0f;
	}

	//TODO: Implement seeking energy pickup
	float mf_seekEnergy(){
		return 0f;
	}

	//Return the best candidate to attack from the given list
	ShipControl getBestTarget(List<ShipControl> targetList){
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
	float evaluateTarget(ShipControl targetShip){
		float value = 0f;
		//Add 0 to 0.5 depending on proximity of ship
		value += (0.5f - 0.5f * Vector2.Distance(this.transform.position,targetShip.transform.position)/awarenessRadius); 
		//Add 0 to 0.5 depending on health of ship (prioritize picking off weaker ships by default)
		value += (0.5f - 0.5f * targetShip.HP / targetShip.maxHP);
		return value;
	}



	//Wander Behaviour
	private void wander(){
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
		//State behaviours
		flee_updateRotation();
		flee_updateThrust();
	}
	
	void flee_updateRotation(){
		//If for some reason there are no targets to flee from, run the fuzzy logic again
		if(fleeShipList.Count <= 0){
			evaluateMembershipFunctions();
			return;
		}

		//Find the center of the enemy formation
		Vector2 enemyBarycenter = Vector2.zero;
		foreach(ShipControl fleeTarget in fleeShipList){
			enemyBarycenter += (Vector2)fleeTarget.transform.position;
		}
		enemyBarycenter = enemyBarycenter / fleeShipList.Count;

		//Orient ourselves to face away from it
		Vector2 vectorToTarget = enemyBarycenter - (Vector2)this.transform.position;
		orientationTarget = (Vector2)this.transform.position - vectorToTarget;
		shipControl.updateRotation(orientationTarget);

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
}
