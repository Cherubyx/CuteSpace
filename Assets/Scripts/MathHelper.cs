using UnityEngine;
using System.Collections;

public class MathHelper {

	public static float getAngleToTarget(Vector3 self, Vector3 target){
		// the vector that we want to measure an angle from
		Vector3 referenceForward = new Vector3(0f,1f,0f);
		
		// the vector perpendicular to referenceForward (90 degrees clockwise)
		// (used to determine if angle is positive or negative)
		Vector3 referenceRight = Vector3.Cross(Vector3.forward, referenceForward);
		
		// the vector of interest
		Vector3 newDirection = target - self;
		
		// Get the angle in degrees between 0 and 180
		float angle = Vector3.Angle(newDirection, referenceForward);
		
		// Determine if the degree value should be negative. Here, a positive value
		// from the dot product means that our vector is on the right of the reference vector
		// whereas a negative value means we're on the left.
		float sign = Mathf.Sign(Vector3.Dot(newDirection, referenceRight));
		
		float finalAngle = sign * angle;
		return finalAngle;
	}

	public static float getRemainingAngleToTarget(GameObject self, Vector3 target){
		Vector2 vectorToTarget = target - self.transform.position;
		return Vector2.Angle (self.transform.up, vectorToTarget);
	}
}
