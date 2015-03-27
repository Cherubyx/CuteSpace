using UnityEngine;
using System.Collections;

public class Weapon_XForm_Laser : Weapon {

	public float damagePerSecond = 5;
	public GameObject Laser1;
	public GameObject Laser2;
	public GameObject ConvergencePoint;
	public LineRenderer line1;
	public LineRenderer line2;
	public Color c1 = Color.yellow;
	public Color c2 = Color.red;
	public ParticleExplosion laserEffect;
	bool lasersActive;
	float laserTimer;
	bool laserTimerIncreasing;
	RaycastHit2D hit1;
	RaycastHit2D hit2;
	
	void Start(){
		laserTimer = 0;
		laserTimerIncreasing = true;
		lasersActive = false;
		line1.SetWidth(0.2f,0.2f);
		line1.SetVertexCount(2);
		line2.SetWidth(0.2f,0.2f);
		line2.SetVertexCount(2);
	}

	void Update(){

		bool laser1hit = false;
		bool laser2hit = false;


		if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)){
			disableLaser();
		}

		if(lasersActive)
		{
			c1 = Color.Lerp(Color.blue,Color.red,laserTimer);
			c2 = Color.Lerp(Color.red,Color.yellow,laserTimer);

			hit1 = Physics2D.Raycast(Laser1.transform.position, (ConvergencePoint.transform.position - Laser1.transform.position).normalized, (ConvergencePoint.transform.position - Laser1.transform.position).magnitude);
			if (hit1.collider != null) {
				ShipControl hit1Ship = hit1.collider.gameObject.GetComponent<ShipControl>();
				if(hit1Ship != null){
					laser1hit = true;
					hit1Ship.takeDamage(damagePerSecond * Time.deltaTime);
					Instantiate(laserEffect,hit1.centroid,Quaternion.identity);
				}
			}

			hit2 = Physics2D.Raycast(Laser2.transform.position, (ConvergencePoint.transform.position - Laser2.transform.position).normalized, (ConvergencePoint.transform.position - Laser2.transform.position).magnitude);
			if (hit2.collider != null) {
				ShipControl hit2Ship = hit2.collider.gameObject.GetComponent<ShipControl>();
				if(hit2Ship != null){
					laser2hit = true;
					hit2Ship.takeDamage(damagePerSecond * Time.deltaTime);
					Instantiate(laserEffect,hit2.centroid,Quaternion.identity);
				}
			}

			if(hit1.collider == null || hit2.collider == null)
			{
				Instantiate(laserEffect,ConvergencePoint.transform.position,Quaternion.identity);
			}
		}

		Vector3 laser1EndPoint = laser1hit ? (Vector3)hit1.centroid : ConvergencePoint.transform.position;
		Vector3 laser2EndPoint = laser2hit ? (Vector3)hit2.centroid : ConvergencePoint.transform.position;

		line1.enabled = lasersActive;
		line2.enabled = lasersActive;
		line1.SetColors(c1,c2);
		line2.SetColors(c1,c2);
		line1.SetPosition(0,Laser1.transform.position);
		line1.SetPosition(1,laser1EndPoint);
		line2.SetPosition(0,Laser2.transform.position);
		line2.SetPosition(1,laser2EndPoint);

		updateLaserTimer();
	}

	override public void fire (){
		activateLaser();
	}

	void activateLaser(){
		//laserTimer = 0;
		//c1 = Color.yellow;
		//c2 = Color.red;

		lasersActive = true;
	}

	void disableLaser(){
		lasersActive = false;
	}

	void updateLaserTimer(){
		if(laserTimerIncreasing){
			laserTimer = Mathf.Min (laserTimer + Time.deltaTime * 6, 1.0f);
		}
		else{
			laserTimer = Mathf.Max (laserTimer - Time.deltaTime * 6, 0.0f);
		}
		
		if(laserTimer <= 0.0f)
		{
			laserTimerIncreasing = true;
		}
		if(laserTimer >= 1.0f)
		{
			laserTimerIncreasing = false;
		}
	}
}
