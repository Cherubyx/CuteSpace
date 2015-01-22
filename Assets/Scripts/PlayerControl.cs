using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	private ShipMovement shipControl;

	// Use this for initialization
	void Start () {
		shipControl = gameObject.GetComponent<ShipMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Counterclockwise Rotation
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
			shipControl.applyCounterClockwiseRotation();
		}
		
		//Clockwise Rotation
		if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
			shipControl.applyClockwiseRotation();
		}
		
		//Forward Thrust
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
			shipControl.applyForwardThrust();
		}
		if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)){
			shipControl.cutThrust();
		}
		if(Input.GetKey(KeyCode.S)){
			shipControl.spaceBrake();
		}

		//Shoot
		if(Input.GetKeyDown(KeyCode.Space)){
			shipControl.fire();
		}
	}
}
