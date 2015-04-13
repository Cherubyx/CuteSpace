using UnityEngine;
using System.Collections;

public class OverworldPlayerAvatar : MonoBehaviour {

	public Sprite dogSprite;
	public Sprite catSprite;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer>().sprite = PersistentGameData.playerRace == "dog" ? dogSprite : catSprite;
		GetComponentInChildren<ParticleSystem>().startColor = PersistentGameData.playerRace == "dog" ? Color.red : new Color(96f/255f,16f/255f,234f/255f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
