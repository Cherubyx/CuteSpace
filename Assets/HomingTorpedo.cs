using UnityEngine;
using System.Collections;

public class HomingTorpedo : MonoBehaviour {

	public GameObject target;
	public float acceleration;
	public float maximumVelocity;

	// Use this for initialization
	void Start () {
		target = GameObject.Find("Pirate");
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 targetVelocity = (target.transform.position  - this.transform.position).normalized * maximumVelocity;
		this.GetComponent<Rigidbody2D>().velocity = Vector2.MoveTowards(this.GetComponent<Rigidbody2D>().velocity, targetVelocity, acceleration * Time.deltaTime);
		/*
		if(newVelocity.magnitude > maximumVelocity){
			newVelocity = newVelocity.normalized * maximumVelocity;
		}
		*/
		 
	
	}
}
