using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Combat : MonoBehaviour {

	public Texture2D cursorImage;
	
	private int cursorWidth = 32;
	private int cursorHeight = 32;

	int rewardCoins;

	// Rumour has it Awake() runs before Start()
	void Awake (){
		//Get a reference to the prefab dictionary
		PrefabDictionary pfd = GameObject.Find("PrefabDictionary").GetComponent<PrefabDictionary>();
		//Instantiate the player's ship
		GameObject playerShip = pfd.instantiatePrefab(PersistentGameData.playerShipName,Vector3.zero,Quaternion.identity);
		//Set the status bar to reflect the player's ship's stats.
		GameObject.Find("Status Bar").GetComponent<StatusBar>().ship = playerShip.GetComponent<ShipControl>();
		//Disable AI Control scripts
		AI_ShipControl[] aiControls = playerShip.GetComponents<AI_ShipControl>();
				foreach(AI_ShipControl aiControl in aiControls){
			aiControl.enabled = false;
		}
		//Add player control script to player's ship
		playerShip.AddComponent<PlayerControl>();
		//Tag the ship to belong to the player
		playerShip.tag = "Player";

		//Load up the XML containing the prefabs to load for this combat scene
		TextAsset combatXML = Resources.Load(PersistentGameData.combatSceneName + "_combat") as TextAsset;
		ScenePrefabCollection scenePrefabCollection = ScenePrefabCollection.LoadFromText(combatXML.ToString());

		//Instantiate each prefab
		foreach(ScenePrefab scenePrefab in scenePrefabCollection.scenePrefabs){
			GameObject newObject = Instantiate(pfd.getPrefab(scenePrefab.prefabName),new Vector2(scenePrefab.x,scenePrefab.y),Quaternion.identity) as GameObject;
			//Tag each new object with its owner
			newObject.tag = scenePrefab.owner;
			//If the object is a jumpgate, set the exit system name
			if(newObject.GetComponent<JumpGate>() != null){
				newObject.GetComponent<JumpGate>().exitSystemName = scenePrefab.exitSystemName;
			}
		}

	}

	void Start () {

		Cursor.visible = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(Input.mousePosition.x - cursorWidth / 2, Screen.height - Input.mousePosition.y - cursorHeight / 2, cursorWidth, cursorHeight), cursorImage);
	}
}
