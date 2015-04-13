using UnityEngine;
using System.Collections;

public class CheezburgerPickup : MonoBehaviour {

	private int amount;
	public GameObject hascheezburger;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			PersistentGameData.cheezburgerCount += amount;
			GameObject.Instantiate(hascheezburger,this.transform.position + new Vector3(-2f, 1f,0f),Quaternion.identity);
			Destroy(this.gameObject);
		}
	}
}
