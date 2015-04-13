using UnityEngine;
using System.Collections;

public class Overworld : MonoBehaviour {

	public Texture2D cursorImage;
	
	private int cursorWidth = 24;
	private int cursorHeight = 24;

	Vector3 origin;
	Vector3 destination;
	public GameObject playerAvatar;
	public GameObject npcAvatar;

	public float timer = 0f;
	bool randomEncounter = false;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		origin = PersistentGameData.overworldOriginPosition;
		destination = GameObject.Find(PersistentGameData.overworldDestinationName).transform.position;
		playerAvatar.transform.position = origin;

		//0.33 repeating of course
		randomEncounter = Random.Range (0f, 1f) < 0.33f;

	}
	
	// Update is called once per frame
	void Update () {

		//If we're going to have a random encounter, wait a second and a half, then spawn an npc avatar
		timer += Time.deltaTime;
		if (randomEncounter && timer > 1.5f) {
			GameObject newObj =  GameObject.Instantiate(npcAvatar,GameObject.Find(PersistentGameData.overworldDestinationName).transform.position,Quaternion.identity) as GameObject;
			OverworldNPCAvatar newAv = newObj.GetComponent<OverworldNPCAvatar>();
			//Choose randomly from the list of NPCs we haven't met yet
			newAv.name = PersistentGameData.npcEncounterPool[Random.Range(0,PersistentGameData.npcEncounterPool.Count)];
			//Remove the chosen string from the list
			PersistentGameData.npcEncounterPool.Remove(newAv.name);
			//If there are none left, reset the list to default.
			if(PersistentGameData.npcEncounterPool.Count <= 0){
				PersistentGameData.npcEncounterPool = PersistentGameData.npcs;
			}
			randomEncounter = false;
		}

		//Move player. Should be in the player avatar class, probably.
		Vector2 dir = destination - playerAvatar.transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		angle -= 90f;
		playerAvatar.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		playerAvatar.transform.position = Vector3.MoveTowards(playerAvatar.transform.position,destination,0.5f * Time.deltaTime);
	}

	void OnGUI()
	{
		//Draw cursor
		GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, cursorWidth, cursorHeight), cursorImage);
	}
}
