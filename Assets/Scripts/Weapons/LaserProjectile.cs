using UnityEngine;
using System.Collections;

public class LaserProjectile : MonoBehaviour {

	public float lifeTime = 1.25f;
	public float damage = 3f;
	public GameObject explosion;
    public string soundName;
	
	// Use this for initialization
	void Start () {
		if(MusicManager.Instance != null){
			MusicManager.Instance.Play(soundName);
		}        
	}
	
	// Update is called once per frame
	void Update () {
		updateLife ();
	}
	
	void updateLife(){
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0f) {
			die ();
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {


		ShipControl ship = other.gameObject.GetComponent<ShipControl>();
		if(ship!=null){						         
			ship.takeDamage(damage);
			Instantiate(explosion,this.transform.position,Quaternion.identity);
			die ();
		}

		//quick and dirty asteroid mining
		Asteroid asteroid = other.gameObject.GetComponent<Asteroid>();
		if(asteroid!=null){						         
			asteroid.takeDamage(damage);
			Instantiate(explosion,this.transform.position,Quaternion.identity);
			die ();
		}

	}
	
	void die(){

		ParticleSystem particleTrail = GetComponentInChildren<ParticleSystem>();
		if(particleTrail != null){
			particleTrail.emissionRate = 0f;
			particleTrail.gameObject.transform.SetParent(null,true);
			particleTrail.gameObject.AddComponent<Die>();
		}

		if(MusicManager.Instance != null){
			MusicManager.Instance.Stop(soundName);
		}
        

		Destroy (this.gameObject);
	}
}
