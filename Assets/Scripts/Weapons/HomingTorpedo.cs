using UnityEngine;
using System.Collections;

public class HomingTorpedo : MonoBehaviour {

	public GameObject target;
	public float acceleration;
	public float maximumVelocity;
	public PersistentGameData.factions torpedoFaction;

	// Use this for initialization
	void Start () {
		ShipControl[] nearbyShips = GameObject.FindObjectsOfType<ShipControl>();
		target = null;
		float shortestDistance = float.MaxValue;

		foreach(ShipControl potentialTarget in nearbyShips){
			if(isEnemy(potentialTarget) && Vector2.Distance(this.transform.position,potentialTarget.transform.position) < shortestDistance){
				target = potentialTarget.gameObject;
				shortestDistance = Vector2.Distance(this.transform.position,potentialTarget.transform.position);
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

	protected bool isEnemy(ShipControl ship){
		return PersistentGameData.factionEnemies[(int)torpedoFaction,(int)ship.faction];
	}

}
