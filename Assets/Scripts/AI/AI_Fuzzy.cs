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
	
	//Flee membership function parameters
	private float bravery_quotient = 2.0f;
	private float energy_evaluation_coefficient = 2.0f;

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
		targetShipList = getNearbyEnemies();

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

	//Attack Behaviour
	private void attack(){
		
		//State behaviours
		attack_updateRotation();
		attack_updateThrust();
		attack_updateFire();
		
	}

	//Wander Behaviour
	private void wander(){
		wander_updateRotation();
		wander_updateThrust();
	}

	//Flee Behaviour
	private void flee(){
		//State behaviours
		flee_updateRotation();
		flee_updateThrust();
	}
}






