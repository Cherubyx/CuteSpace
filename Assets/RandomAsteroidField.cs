using UnityEngine;
using System.Collections;

public class RandomAsteroidField : MonoBehaviour {

	public GameObject randomAsteroid;
	public int numberOfAsteroids = 50;
	public float fieldWidth = 100;
	public float fieldHeight = 100;

	// Use this for initialization
	void Start () {
		for(int i=0; i<numberOfAsteroids; i++){
			Vector2 pos = new Vector2(Random.Range(-fieldWidth/2f,fieldWidth/2f),Random.Range(-fieldHeight/2f,fieldHeight/2f));
			GameObject.Instantiate(randomAsteroid,pos,Quaternion.identity);
		}
	}
}
