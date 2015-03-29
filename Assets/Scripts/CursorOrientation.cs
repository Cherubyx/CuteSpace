using UnityEngine;
using System.Collections;

public class CursorOrientation : MonoBehaviour {

	public GameObject target;
	public float turnSpeed = 3;

	// Use this for initialization
	void Start () {
		target = GameObject.Find("cursor");
	}
	
	// Update is called once per frame
	void Update () {
		updateRotation();
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

	void updateRotation(){
		float targetAngle = getAngleToTarget();
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler (0, 0, targetAngle), turnSpeed * Time.deltaTime);
	}
}
