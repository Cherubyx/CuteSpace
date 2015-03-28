using UnityEngine;
using System.Collections;

public class AI_Pirate : MonoBehaviour {

	enum AI_state {wander,flee,attack,strafe};
	AI_state currentState;

	public Vector2 orientationTarget;
	public GameObject targetGameObject;
	public float turnSpeed = 3;


	//Wander parameters
	public float desiredDistance = 5.0f;

	public float closingDistance = 3f;
	public float brakingDistance = 30f;
	public float forwardThrustTargetAngle = 45f;

	public float firingDistance = 10f;
	public float firingArc = 45f;
	
	private ShipControl shipControl;

	// Use this for initialization
	void Start () {
		currentState = AI_state.attack;
		orientationTarget = new Vector2(0f,0f);
		shipControl = this.gameObject.GetComponent<ShipControl>();
	}
	
	// Update is called once per frame
	void Update () {
		switch(currentState)
		{
			case AI_state.wander:
				wander();
				break;
			case AI_state.flee:
				flee();
				break;
			case AI_state.attack:
				attack();
				break;
			case AI_state.strafe:
				strafe();
				break;
			default:
				wander();
				break;
		}

	}

	private void wander(){

	}

	private void flee(){

	}

	private void attack(){
		orientationTarget = targetGameObject.transform.position;
		attack_updateRotation();
		attack_updateThrust();
		attack_updateFire();

	}

	private void strafe(){

	}

	void attack_updateRotation(){
		float targetAngle = MathHelper.getAngleToTarget(this.transform.position,orientationTarget);
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler (0, 0, targetAngle), turnSpeed * Time.deltaTime);
	}

	void attack_updateThrust(){
		if(Vector2.Distance (this.transform.position, targetGameObject.transform.position) > closingDistance && Mathf.Abs (MathHelper.getRemainingAngleToTarget(this.gameObject,targetGameObject.transform.position)) < forwardThrustTargetAngle) {
			shipControl.applyForwardThrust ();
		}
		else {
			shipControl.cutThrust();
		}	

		if(Vector2.Distance(this.transform.position,targetGameObject.transform.position) < brakingDistance){
			shipControl.spaceBrake();
		}

	}

	void attack_updateFire(){
		if(Vector2.Distance(this.transform.position,targetGameObject.transform.position) < firingDistance){
			shipControl.firePrimaryWeapons();
		}
	}
}
