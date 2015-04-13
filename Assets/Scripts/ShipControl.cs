//This is the generic ship control class. Ship control classes for individual ships can be derived from this class and override its methods when needed.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipControl : MonoBehaviour {

	//Ship type name to display
	public string shipClassName;

	public List<GameObject> potentialDrops;

	//To which faction does this ship belong? Player, Cat, Dog, Pirate, Merchant, Superhostile?
	public PersistentGameData.factions faction;

	//Maximum hit points, energy rate, and energy regeneration
	public float maxHP;
	public float maxEnergy;
	public float energyGenerationRate;

	//Handling characteristics
	public float mainThrusterForce;
	public float maximumVelocity;
	public float comstabDrag;
	public float maneuveringThrusterForceRatio = 0.2f;
	//TODO: Use Yaw or Turnspeed?
	public float yaw;
	public float turnSpeed = 3f;

	//How many particles per second should the thrusters emit?
	public float thrusterParticleEmissionRate;
	//Reference to the thruster particle system
	public List<ParticleSystem> mainEngineExhaustEmissionPorts;
	public List<ParticleSystem> portThrusterExhaustEmissionPorts;
	public List<ParticleSystem> starboardThrusterExhaustEmissionPorts;
	public List<ParticleSystem> retroThrusterExhaustEmissionPorts;

	//Current HP and energy levels
	public float HP;
	public float energy;

	//explosion animation to play on death
	public GameObject onDeathExplosion;

	//Flag to see if the engines are currently on
	private bool mainEnginesOn;
	private bool portThrustersOn;
	private bool starboardThrustersOn;
	private bool retroThrustersOn;
	private bool spaceBrakeOn;

	//Reference to the weapon objects
	public List<Weapon> primaryWeapons;
	public List<Weapon> secondaryWeapons;

	//Sometimes energy can be supercharged!
	private float superChargeTimer = 0f;

	
	// Use this for initialization
	void Start () {
		HP = maxHP;
		energy = maxEnergy;
		mainEnginesOn = false;
		portThrustersOn = false;
		starboardThrustersOn = false;
		retroThrustersOn = false;
	}
	
	// Update is called once per frame
	void Update () {
		energy = Mathf.Min (energy + energyGenerationRate * Time.deltaTime,maxEnergy);
		if (superChargeTimer > 0f ) {
			energy = Mathf.Min (energy + energyGenerationRate * 4 * Time.deltaTime,maxEnergy);
			superChargeTimer -= Time.deltaTime;
		}
		updateThrust ();
	}

	//Toggles main engines on
	public virtual void activateMainEngines() {
		if(!mainEnginesOn){
			mainEnginesOn = true;
			foreach(ParticleSystem thruster in mainEngineExhaustEmissionPorts){
				thruster.emissionRate = thrusterParticleEmissionRate;
			}
		}
	}

	//Toggles main engines off
	public virtual void cutMainEngines(){
		if(mainEnginesOn){
			mainEnginesOn = false;
			foreach(ParticleSystem thruster in mainEngineExhaustEmissionPorts){
				thruster.emissionRate = 0f;
			}
		}
	}

	//Toggles port thrusters on
	public virtual void activatePortThrusters(){
		if(!portThrustersOn){
			portThrustersOn = true;
			foreach(ParticleSystem thruster in portThrusterExhaustEmissionPorts){
				thruster.emissionRate = thrusterParticleEmissionRate;
			}
		}
	}

	//Toggles port thrusters off
	public virtual void cutPortThrusters(){
		if(portThrustersOn){
			portThrustersOn = false;
			foreach(ParticleSystem thruster in portThrusterExhaustEmissionPorts){
				thruster.emissionRate = 0f;
			}
		}
	}

	//Toggles starboard thrusters on
	public virtual void activateStarboardThrusters(){
		if(!starboardThrustersOn){
			starboardThrustersOn = true;
			foreach(ParticleSystem thruster in starboardThrusterExhaustEmissionPorts){
				thruster.emissionRate = thrusterParticleEmissionRate;
			}
		}
	}

	//Toggles starboard thrusters off
	public virtual void cutStarboardThrusters(){
		if(starboardThrustersOn){
			starboardThrustersOn = false;
			foreach(ParticleSystem thruster in starboardThrusterExhaustEmissionPorts){
				thruster.emissionRate = 0f;
			}
		}
	}

	//Toggles retro thrusters on
	public virtual void activateRetroThrusters(){
		if(!retroThrustersOn){
			retroThrustersOn = true;
			foreach(ParticleSystem thruster in retroThrusterExhaustEmissionPorts){
				thruster.emissionRate = thrusterParticleEmissionRate;
			}
		}
	}

	//Toggles retro thrusters off
	public virtual void cutRetroThrusters(){
		if(retroThrustersOn){
			retroThrustersOn = false;
			foreach(ParticleSystem thruster in retroThrusterExhaustEmissionPorts){
				thruster.emissionRate = 0f;
			}
		}
	}

	//Toggles space brake on
	//Enables drag to slow the ship when engines are off
	public virtual void activateSpaceBrake() {
		spaceBrakeOn = true;
	}

	//Toggles space brake off
	public virtual void cutSpaceBrake() {
		spaceBrakeOn = false;
	}


	//Apply forces from activated thrusters
	public virtual void updateThrust(){

		//If thrusters are on, we want drag for nice handling. Otherwise, we want space-y drifting.
		if(mainEnginesOn || spaceBrakeOn){
			this.GetComponent<Rigidbody2D>().drag = comstabDrag;
		}
		else if(portThrustersOn || starboardThrustersOn || retroThrustersOn){
			this.GetComponent<Rigidbody2D>().drag = comstabDrag * maneuveringThrusterForceRatio;
		}
		else{
			this.GetComponent<Rigidbody2D>().drag = 0f;
		}

		//Check thruster status and add appropriate force
		if (mainEnginesOn) {
			this.GetComponent<Rigidbody2D> ().AddForce (this.transform.up * mainThrusterForce * Time.deltaTime);
		}
		if (portThrustersOn){
			this.GetComponent<Rigidbody2D> ().AddForce (this.transform.right * mainThrusterForce * maneuveringThrusterForceRatio * Time.deltaTime);
		}
		if (starboardThrustersOn){
			this.GetComponent<Rigidbody2D> ().AddForce (-this.transform.right * mainThrusterForce * maneuveringThrusterForceRatio * Time.deltaTime);
		}
		if (retroThrustersOn){
			this.GetComponent<Rigidbody2D> ().AddForce (-this.transform.up * mainThrusterForce * maneuveringThrusterForceRatio * Time.deltaTime);
		}
	}

	//Make the ship turn to face the orientation target
	public virtual void updateRotation(Vector2 orientationTarget){
		float targetAngle = MathHelper.getAngleToTarget(this.transform.position,orientationTarget);
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler (0, 0, targetAngle), turnSpeed * Time.deltaTime);

	}



	//Disables drag so that ships can drift on whatever vector they were on after engines are disengaged
	void cancelDrag() {
		this.GetComponent<Rigidbody2D>().drag = 0f;
	}

	//Fires all weapons in primary weapon group
	public virtual void firePrimaryWeapons() {
		foreach(Weapon weapon in primaryWeapons){
			if(weapon.energyCost < energy && weapon.remainingCooldown <= 0f){
				energy -= weapon.energyCost;
				weapon.fire();
			}
		}
	}

	//Fires all weapons in secondary weapon group
	public virtual void fireSecondaryWeapons() {
		foreach(Weapon weapon in secondaryWeapons){
			if(weapon.energyCost < energy && weapon.remainingCooldown <= 0f){
				energy -= weapon.energyCost;
				weapon.fire();
			}
		}
	}

	//Disengage channelled weapons in primary weapon group
	public virtual void ceaseFirePrimaryWeapons(){
		foreach(Weapon weapon in primaryWeapons){
			weapon.ceaseFire();
		}
	}

	//Disengage channelled weapons in secondary weapon group
	public virtual void ceaseFireSecondaryWeapons() {
		foreach(Weapon weapon in secondaryWeapons){
			weapon.ceaseFire();
		}
	}

	public void activateSuperCharge(float chargeDuration){
		superChargeTimer = chargeDuration;
	}

	//TODO: refactor to generic 'damageable' class
	public virtual void takeDamage(float damage){
		HP -= damage;
		Debug.Log ("HP: " + HP);
		if(HP <= 0f){
			die ();
		}
	}

	//TODO: refactor to generic 'damageable' class
	public virtual void die(){
		Debug.Log("I'm Dead!");
		if (potentialDrops.Count > 0) {
			GameObject.Instantiate(potentialDrops[Random.Range(0,potentialDrops.Count)],this.transform.position,Quaternion.identity);
		}

		Instantiate(onDeathExplosion,this.transform.position,Quaternion.identity);
		Destroy(this.gameObject);
	}

	//Deprecated
	public virtual void applyCounterClockwiseRotation() {
		//cancelRotation ();
		this.transform.Rotate (0f, 0f, yaw * Time.deltaTime);
		//this.GetComponent<Rigidbody2D> ().angularVelocity = 10f;
	}
	
	//Deprecated
	public virtual void applyClockwiseRotation(){
		//cancelRotation ();
		this.transform.Rotate (0f, 0f, -yaw * Time.deltaTime);
		//this.GetComponent<Rigidbody2D> ().angularVelocity = -10f;
	}
	
	//Deprecated
	void cancelRotation() {
		this.GetComponent<Rigidbody2D>().angularVelocity = 0;
	}


}

