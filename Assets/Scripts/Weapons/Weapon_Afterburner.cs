using UnityEngine;
using System.Collections;

public class Weapon_Afterburner : Weapon {

	public GameObject afterburnerExhaust;
	public GameObject exhaustPort;
	private bool active;
	private ShipControl ship;
	private float baselineThrust;
	private float exhaustDelay = 0.05f;
	private float exhaustTimer = 0f;
	private float energyRate = 6.0f;


	// Use this for initialization
	void Start () {
		active = false;
		ship = GetComponent<ShipControl>();
		baselineThrust = ship.mainThrusterForce;
	}
	
	// Update is called once per frame
	void Update () {
		exhaustTimer -= Time.deltaTime;
		if(ship.energy <= 0.5f){
			ceaseFire();
		}
		if(active){
			ship.energy -= energyRate * Time.deltaTime;
			if(exhaustTimer <= 0f){
				GameObject exhaust = GameObject.Instantiate(afterburnerExhaust,exhaustPort.transform.position,transform.localRotation) as GameObject;
				Physics2D.IgnoreCollision(ship.gameObject.GetComponent<Collider2D>(),exhaust.GetComponent<Collider2D>());
				exhaustTimer = exhaustDelay;
			}
		}
	}

	override public void fire(){
		if(ship.energy > 2.0f){
			active=true;
			ship.mainThrusterForce = baselineThrust * 2.0f;
			ship.activateMainEngines();
		}
	}
	override public void ceaseFire(){
		active=false;
		ship.mainThrusterForce = baselineThrust;
		ship.cutMainEngines();
	}
}
