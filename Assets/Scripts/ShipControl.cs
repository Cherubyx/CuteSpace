//This is the generic ship control class. Ship control classes for individual ships should be derived from this class and override its methods when needed.
using UnityEngine;
using System.Collections;

public class ShipControl : MonoBehaviour {

	//Maximum hit points, energy rate, and energy regeneration
	public float maxHP;
	public float maxEnergy;
	public float energyGenerationRate;

	//Handling characteristics
	public float mainThrusterForce;
	public float maximumVelocity;
	public float comstabDrag;
	public float yaw;

	//How many particles per second should the thrusters emit?
	public float thrusterParticleEmissionRate;
	//Reference to the thruster particle system
	public ParticleSystem thrusterParticleSystem;

	//Current HP and energy levels
	private float HP;
	private float energy;

	public ParticleExplosion onDeathExplosion;

	//Reference to the weapon object
	public Weapon weapon;
	
	// Use this for initialization
	void Start () {
		HP = maxHP;
		energy = maxEnergy;
	}
	
	// Update is called once per frame
	void Update () {
		energy = Mathf.Min (energy + energyGenerationRate * Time.deltaTime,maxEnergy);
	}
	
	public virtual void applyForwardThrust() {
		if(this.GetComponent<Rigidbody2D>().velocity.magnitude < maximumVelocity){
			this.GetComponent<Rigidbody2D>().AddForce(this.transform.up * mainThrusterForce * Time.deltaTime);
		}
		this.GetComponent<Rigidbody2D>().drag = comstabDrag;
		thrusterParticleSystem.emissionRate = thrusterParticleEmissionRate;
	}
	
	public virtual void cutThrust(){
		cancelDrag();
		thrusterParticleSystem.emissionRate = 0f;
	}
	
	public virtual void applyCounterClockwiseRotation() {
		this.transform.Rotate (0f, 0f, yaw * Time.deltaTime);
	}
	
	public virtual void applyClockwiseRotation(){
		this.transform.Rotate (0f, 0f, -yaw * Time.deltaTime);
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
	
	public virtual void fire() {
		weapon.fire();
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

