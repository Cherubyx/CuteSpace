using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {
	
	public float mainThrusterForce;
	public float maximumVelocity;
	public float comstabDrag;
	public float yaw;

	public float thrusterParticleEmissionRate;

	public ParticleSystem thrusterParticleSystem;

	public Weapon weapon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
			
	}

	public void applyForwardThrust() {
		if(this.rigidbody2D.velocity.magnitude < maximumVelocity){
			this.rigidbody2D.AddForce(this.transform.up * mainThrusterForce * Time.deltaTime);
		}
		this.rigidbody2D.drag = comstabDrag;
		thrusterParticleSystem.emissionRate = thrusterParticleEmissionRate;
	}

	public void cutThrust(){
		cancelDrag();
		thrusterParticleSystem.emissionRate = 0f;
	}

	public void applyCounterClockwiseRotation() {
		this.transform.Rotate (0f, 0f, yaw * Time.deltaTime);
	}

	public void applyClockwiseRotation(){
		this.transform.Rotate (0f, 0f, -yaw * Time.deltaTime);
	}

	void cancelRotation() {
		this.rigidbody2D.angularVelocity = 0;
	}

	void cancelDrag() {
		this.rigidbody2D.drag = 0f;
	}

	public void spaceBrake() {
		this.rigidbody2D.drag = comstabDrag;
	}

	public void fire() {
		weapon.fire();
	}
}
