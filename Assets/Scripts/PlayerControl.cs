using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	private ShipControl shipControl;

	// Use this for initialization
	void Start () {
		//shipControl = gameObject.GetComponent<ShipMovement> ();
		shipControl = gameObject.GetComponent<ShipControl> ();
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
		if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
			shipControl.applyForwardThrust();
		}
		if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)){
			shipControl.cutThrust();
		}
		if(Input.GetKey(KeyCode.S)){
			shipControl.spaceBrake();
		}

		//Shoot. While the button is held down, this will invoke the method every update.
		if(Input.GetMouseButton(0)){
			shipControl.firePrimaryWeapons();
		}

		if(Input.GetMouseButton(1)){
			shipControl.fireSecondaryWeapons();
		}

		//Stop shooting... sometimes relevant
		if(Input.GetMouseButtonUp(0)){
			shipControl.ceaseFirePrimaryWeapons();
		}
		
		if(Input.GetMouseButtonUp(1)){
			shipControl.ceaseFireSecondaryWeapons();
		}
	}
}
