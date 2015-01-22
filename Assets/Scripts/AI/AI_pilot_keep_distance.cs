using UnityEngine;
using System.Collections;

public class AI_pilot_keep_distance : MonoBehaviour {

	public float flee_radius = 8f;
	public float seek_radius = 12f;
	public float forwardThrustTargetAngle = 45f;
	private GameObject target;
	private Ai_pilot_evade AI_evade;
	private AI_pilot_seek AI_seek;
		
	private int state;
	
	// Use this for initialization
	void Start () {
		target = GameObject.FindGameObjectWithTag("Player");
		AI_seek = this.GetComponent<AI_pilot_seek>();
		AI_evade = this.GetComponent<Ai_pilot_evade>();
	}
	
	// Update is called once per frame
	void Update () {
		updateState();
	}
	
		//Seek, Hold, Flee
	void updateState() {
		if(Vector2.Distance(this.transform.position,target.transform.position) < flee_radius){
			AI_evade.enabled = true;
			AI_seek.enabled = false;
		}
		if(Vector2.Distance(this.transform.position,target.transform.position) > seek_radius){
			AI_evade.enabled = false;
			AI_seek.enabled = true;
		}
	}
}
