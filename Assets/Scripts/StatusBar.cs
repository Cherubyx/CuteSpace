using UnityEngine;
using System.Collections;

public class StatusBar : MonoBehaviour {

	public ShipControl ship;
	public TextMesh text;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(ship != null){
			text.text = ship.shipClassName + " ("+ship.faction+")";
		}
		else{
			Destroy (this.gameObject);
		}	
	}
}
