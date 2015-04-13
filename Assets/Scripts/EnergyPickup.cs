using UnityEngine;
using System.Collections;

public class EnergyPickup : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other) {
		ShipControl ship = other.GetComponent<ShipControl> ();
		if (ship != null ) {
			ship.activateSuperCharge(5.0f);
		}
		Destroy (this.gameObject);
	}
}
