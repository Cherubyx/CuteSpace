using UnityEngine;
using System.Collections;

public class LargeHomingMissile : MonoBehaviour {

	public float launchForce = 100f;
	float maxVelocity = 3f;
	public float lifeTime = 3f;
	public string targetTeam;
	public GameObject target;
	public ParticleExplosion explosion;

	// Use this for initialization
	void Start () {
		//targetTeam = "Enemy";
		//target = this.gameObject;
		//setTarget ();
		//this.GetComponent<Rigidbody2D> ().AddForce (this.transform.up * launchForce);
	}
	
	// Update is called once per frame
	void Update () {
		updateRotation ();
		updateVelocity ();
		updateLife ();
	}

	public void setTargetTeam(string targetTeamName){
		targetTeam = targetTeamName;
		setTarget ();
	}

	void setTarget(){
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag (targetTeam);
		GameObject closestPotentialTarget = this.gameObject;
		if (gameObjects.Length > 0) {
			closestPotentialTarget = gameObjects[0];
		}
		foreach(GameObject potentialTarget in gameObjects){
			if((potentialTarget.transform.position - this.transform.position).magnitude < (closestPotentialTarget.transform.position - this.transform.position).magnitude){
				closestPotentialTarget = potentialTarget;
			}
		}
		target = closestPotentialTarget;
	}	

	//Absolute angle to target
	float getAngleToTarget(){
		// the vector that we want to measure an angle from
		Vector3 referenceForward = new Vector3(0f,1f,0f);
		
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

	//Difference between object's angle and target angle
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
		float targetAngle = getAngleToTarget();
		float turnSpeed = 1;
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, targetAngle), turnSpeed * Time.deltaTime);
	}
	
	void updateVelocity(){
		this.gameObject.GetComponent<Rigidbody2D>().velocity = this.transform.up * maxVelocity;
	}

	void updateLife(){
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0f) {
			die ();
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		die ();
	}

	void die(){
		Instantiate(explosion,this.transform.position,Quaternion.identity);
		Destroy (this.gameObject);
	}
}
