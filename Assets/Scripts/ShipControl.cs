//This is the generic ship control class. Ship control classes for individual ships should be derived from this class and override its methods when needed.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipControl : MonoBehaviour {

	//Maximum hit points, energy rate, and energy regeneration
	public float maxHP;
	public float maxEnergy;
	public float energyGenerationRate;

	//Handling characteristics
	public float mainThrusterForce;
	public float maximumVelocity;
	public float comstabDrag;
	//TODO: Use Yaw or Turnspeed?
	public float yaw;
	public float turnSpeed = 3f;

	//How many particles per second should the thrusters emit?
	public float thrusterParticleEmissionRate;
	//Reference to the thruster particle system
	public List<ParticleSystem> thrusterExhaustEmissionPorts;

	//Current HP and energy levels
	public float HP;
	public float energy;

	//explosion animation to play on death
	public GameObject onDeathExplosion;

	//Flag to see if the engines are currently on
	private bool enginesOn;

	//Reference to the weapon objects
	public List<Weapon> primaryWeapons;
	public List<Weapon> secondaryWeapons;
	
	// Use this for initialization
	void Start () {
		HP = maxHP;
		energy = maxEnergy;
		enginesOn = false;
	}
	
	// Update is called once per frame
	void Update () {
		energy = Mathf.Min (energy + energyGenerationRate * Time.deltaTime,maxEnergy);
		updateThrust ();
	}
	
	public virtual void applyForwardThrust() {
		if(!enginesOn){
			enginesOn = true;
			this.GetComponent<Rigidbody2D>().drag = comstabDrag;
			foreach(ParticleSystem thruster in thrusterExhaustEmissionPorts){
				thruster.emissionRate = thrusterParticleEmissionRate;
			}
		}
	}

	public virtual void updateThrust(){
		if (enginesOn) {
			if (this.GetComponent<Rigidbody2D> ().velocity.magnitude < maximumVelocity) {
					this.GetComponent<Rigidbody2D> ().AddForce (this.transform.up * mainThrusterForce * Time.deltaTime);
			}
		}
	}
	
	public virtual void cutThrust(){
		if(enginesOn){
			enginesOn = false;
			cancelDrag();
			foreach(ParticleSystem thruster in thrusterExhaustEmissionPorts){
				thruster.emissionRate = 0f;
			}
		}
	}

	public virtual void updateRotation(Vector2 orientationTarget){
		float targetAngle = MathHelper.getAngleToTarget(this.transform.position,orientationTarget);
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler (0, 0, targetAngle), turnSpeed * Time.deltaTime);
	}
	
	public virtual void applyCounterClockwiseRotation() {
		//cancelRotation ();
		this.transform.Rotate (0f, 0f, yaw * Time.deltaTime);
		//this.GetComponent<Rigidbody2D> ().angularVelocity = 10f;
	}
	
	public virtual void applyClockwiseRotation(){
		//cancelRotation ();
		this.transform.Rotate (0f, 0f, -yaw * Time.deltaTime);
		//this.GetComponent<Rigidbody2D> ().angularVelocity = -10f;
	}
	
	void cancelRotation() {
		this.GetComponent<Rigidbody2D>().angularVelocity = 0;
	}
	
	void cancelDrag() {
		this.GetComponent<Rigidbody2D>().drag = 0f;
	}
	
	public virtual void spaceBrake() {
		this.GetComponent<Rigidbody2D>().drag = comstabDrag;
	}

	public virtual void firePrimaryWeapons() {
		foreach(Weapon weapon in primaryWeapons){
			if(weapon.energyCost < energy && weapon.remainingCooldown <= 0f){
				energy -= weapon.energyCost;
				weapon.fire();
			}
		}
	}

	public virtual void fireSecondaryWeapons() {
		foreach(Weapon weapon in secondaryWeapons){
			if(weapon.energyCost < energy && weapon.remainingCooldown <= 0f){
				energy -= weapon.energyCost;
				weapon.fire();
			}
		}
	}

	public virtual void takeDamage(float damage){
		HP -= damage;
		Debug.Log ("HP: " + HP);
		if(HP <= 0f){
			die ();
		}
	}

	public virtual void die(){
		Debug.Log("I'm Dead!");
		Instantiate(onDeathExplosion,this.transform.position,Quaternion.identity);
		Destroy(this.gameObject);
	}
}

