using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

	public GameObject dogeCoin;
	public float dropChance;
	public float HP;
	public float maxHP;
	public GameObject onDeathExplosion;

	//TODO: refactor to generic 'damageable' class
	public virtual void takeDamage(float damage){
		HP -= damage;
		if(HP <= 0f){
			die ();
		}
	}
	
	//TODO: refactor to generic 'damageable' class
	public virtual void die(){
		Debug.Log("I'm Dead!");
		if (Random.Range(0f,1f) < dropChance) {
			GameObject.Instantiate(dogeCoin,this.transform.position,Quaternion.identity);
		}
		
		Instantiate(onDeathExplosion,this.transform.position,Quaternion.identity);
		Destroy(this.gameObject);
	}

	// Use this for initialization
	void Start () {
		HP = maxHP;	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
