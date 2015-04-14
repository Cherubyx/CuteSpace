using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomAsteroid : MonoBehaviour {

	public List<GameObject> asteroidList;

	// Use this for initialization
	void Start () {
		GameObject.Instantiate(asteroidList[Random.Range(0,asteroidList.Count)],this.transform.position,Quaternion.identity);
		Destroy(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
