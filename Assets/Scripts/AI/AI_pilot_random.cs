using UnityEngine;
using System.Collections;

public class AI_pilot_random : MonoBehaviour {

	public ShipMovement shipControl;
	private int state;
	private float stateChangeTimer = 0f;
	// Use this for initialization
	void Start () {
		shipControl = gameObject.GetComponent<ShipMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) 
		{
			case 0:
				shipControl.applyForwardThrust ();
				break;
			case 1:
				shipControl.applyClockwiseRotation ();
				break;
			case 2:
				shipControl.applyCounterClockwiseRotation ();
				break;
			case 3:
				shipControl.spaceBrake ();
				break;
		}

		updateState ();
		stateChangeTimer -= Time.deltaTime;
	}

	void updateState(){
		if (stateChangeTimer < 0f) {
			state = Random.Range(0,3);
			stateChangeTimer = Random.Range(1f,5f);
		}
	}
}
