using UnityEngine;
using System.Collections;

public class DogecoinPickup : MonoBehaviour {

	private int amount;
	public GameObject dogeGet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			PersistentGameData.dogecoinCount += amount;
			GameObject.Instantiate(dogeGet,this.transform.position + new Vector3(-1f, 1f,0f),Quaternion.identity);
			Destroy(this.gameObject);
		}
	}


}
