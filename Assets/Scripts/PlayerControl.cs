using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	private ShipControl shipControl;

	// Use this for initialization
	void Start () {
		shipControl = gameObject.GetComponent<ShipControl> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		//Orient ship to face mouse position
		shipControl.updateRotation(Camera.main.ScreenToWorldPoint(Input.mousePosition));

		//thrust to port
		if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
			shipControl.activateStarboardThrusters();
		}

		if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)){
			shipControl.cutStarboardThrusters();
		}
		
		//thrust to starboard
		if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
			shipControl.activatePortThrusters();
		}

		if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)){
			shipControl.cutPortThrusters();
		}

		//retrograde thrust
		if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
			shipControl.activateRetroThrusters();
		}

		if(Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)){
			shipControl.cutRetroThrusters();
		}
		
		//Forward Thrust
		if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
			shipControl.activateMainEngines();
		}
		if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)){
			shipControl.cutMainEngines();
		}

		//Space brake
		if(Input.GetKeyDown(KeyCode.Space)){
			shipControl.activateSpaceBrake();
		}

		if(Input.GetKeyUp(KeyCode.Space)){
			shipControl.cutSpaceBrake();
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
