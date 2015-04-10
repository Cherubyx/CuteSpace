using UnityEngine;
using System.Collections;

public class HomingTorpedo : MonoBehaviour {

	public GameObject target;
	public float acceleration;
	public float maximumVelocity;

	// Use this for initialization
	void Start () {
		GameObject[] potentialTargets = GameObject.FindGameObjectsWithTag("Team2");
		if(potentialTargets.Length > 0){
			target = potentialTargets[0];
		}

		foreach(GameObject potentialTarget in potentialTargets){
			if(Vector2.Distance(this.transform.position,potentialTarget.transform.position) < Vector2.Distance(this.transform.position,target.transform.position)){
				target = potentialTarget;
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 targetVelocity = Vector2.zero;
		if(target != null){
			targetVelocity = (target.transform.position  - this.transform.position).normalized * maximumVelocity;

		}
		else{
			targetVelocity = this.transform.up.normalized * maximumVelocity;
		}

		this.GetComponent<Rigidbody2D>().velocity = Vector2.MoveTowards(this.GetComponent<Rigidbody2D>().velocity, targetVelocity, acceleration * Time.deltaTime);
	}
}
