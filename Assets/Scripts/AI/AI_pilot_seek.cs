using UnityEngine;
using System.Collections;

public class AI_pilot_seek : MonoBehaviour {

	public float closingDistance = 15f;
	public float brakingDistance = 30f;
	public float forwardThrustTargetAngle = 45f;

	private ShipControl shipControl;
	private float stateChangeTimer = 0f;
	private GameObject target;

	// Use this for initialization
	void Start () {
		shipControl = gameObject.GetComponent<ShipControl> ();
		target = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		updateRotation ();
		updateThrust ();
	}

	float getAngleToTarget(){
		Vector2 vectorToTarget = target.transform.position - this.transform.position;
		return Vector2.Angle (this.transform.up, vectorToTarget);
	}

	float getAngleToTarget2(){
		// the vector that we want to measure an angle from
		Vector3 referenceForward = this.transform.up;
			
		// the vector perpendicular to referenceForward (90 degrees clockwise)
		// (used to determine if angle is positive or negative)
		Vector3 referenceRight = Vector3.Cross(Vector3.forward, referenceForward);
		
		// the vector of interest
		Vector3 newDirection = target.transform.position - this.transform.position;
			
		// Get the angle in degrees between 0 and 180
		float angle = Vector3.Angle(newDirection, referenceForward);
		
		// Determine if the degree value should be negative. Here, a positive value
		// from the dot product means that our vector is on the right of the reference vector
		// whereas a negative value means we're on the left.
		float sign = Mathf.Sign(Vector3.Dot(newDirection, referenceRight));
		
		float finalAngle = sign * angle;
		return finalAngle;
	}

	void updateRotation(){
		//Debug.Log ("Angle to target is: " + getAngleToTarget2 ());
		if (getAngleToTarget2 () > 10) {
			shipControl.applyCounterClockwiseRotation ();
		} else if (getAngleToTarget2 () < -10) {
			shipControl.applyClockwiseRotation() ;
		}
	}

	void updateThrust(){
		if (Vector2.Distance (this.transform.position, target.transform.position) > closingDistance && Mathf.Abs (getAngleToTarget ()) < forwardThrustTargetAngle) {
			shipControl.applyForwardThrust ();
		} 
		else {
			shipControl.cutThrust();
		}

		if(Vector2.Distance(this.transform.position,target.transform.position) < brakingDistance){
			shipControl.spaceBrake();
		}
	}
}