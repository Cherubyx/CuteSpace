using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Combat : MonoBehaviour {

	public Texture2D cursorImage;
	public GameObject statusBar;
	public Transform canvas;
	public StatusBar playerStatusBar;
	public List<StatusBar> allyStatusBars;
	public List<StatusBar> enemyStatusBars;
	int allyStatusBarCount = 0;
	int enemyStatusBarCount = 0;
	
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
		playerStatusBar.ship = playerShip.GetComponent<ShipControl> ();
		//Disable AI Control scripts
		AI_ShipControl[] aiControls = playerShip.GetComponents<AI_ShipControl>();
				foreach(AI_ShipControl aiControl in aiControls){
			aiControl.enabled = false;
		}
		//Add player control script to player's ship
		playerShip.AddComponent<PlayerControl>();
		//Tag the ship to belong to the player
		playerShip.tag = "Player";
		//Set the player's ship faction to cat or dog
		playerShip.GetComponent<ShipControl>().faction = (PersistentGameData.factions)Enum.Parse(typeof(PersistentGameData.factions),PersistentGameData.playerRace.ToLower());



		//Spawn the player's allies
		foreach (string shipName in PersistentGameData.playerFleet) {
			GameObject allyship = pfd.instantiatePrefab(shipName,new Vector3(UnityEngine.Random.Range(-5f,5f),UnityEngine.Random.Range(-5f,5f),0f),Quaternion.identity);
			allyship.GetComponent<ShipControl>().faction = (PersistentGameData.factions)Enum.Parse(typeof(PersistentGameData.factions),PersistentGameData.playerRace.ToLower());
			//allyStatusBars[allyStatusBarCount].enabled = true;
			allyStatusBars[allyStatusBarCount].ship = allyship.GetComponent<ShipControl> ();
			allyStatusBarCount = allyStatusBarCount+1 % allyStatusBars.Count;
		}

		//Load up the XML containing the prefabs to load for this combat scene
		TextAsset combatXML = Resources.Load(PersistentGameData.combatSceneName + "_combat") as TextAsset;
		ScenePrefabCollection scenePrefabCollection = ScenePrefabCollection.LoadFromText(combatXML.ToString());

		//Instantiate each prefab
		foreach(ScenePrefab scenePrefab in scenePrefabCollection.scenePrefabs){
			GameObject newObject = Instantiate(pfd.getPrefab(scenePrefab.prefabName),new Vector2(scenePrefab.x,scenePrefab.y),Quaternion.identity) as GameObject;
			//If the object is a ship, set the ship's faction
			if(newObject.GetComponent<ShipControl>() != null){
				newObject.GetComponent<ShipControl>().faction = (PersistentGameData.factions)Enum.Parse(typeof(PersistentGameData.factions),scenePrefab.faction);
				//statusBars[statusBarCount].enabled = true;
				if(PersistentGameData.areEnemies((int)playerShip.GetComponent<ShipControl>().faction,(int)newObject.GetComponent<ShipControl>().faction)){
					enemyStatusBars[enemyStatusBarCount].ship = newObject.GetComponent<ShipControl> ();
					enemyStatusBarCount = enemyStatusBarCount+1 % enemyStatusBars.Count;
				}
				else{
					allyStatusBars[allyStatusBarCount].ship = newObject.GetComponent<ShipControl> ();
					allyStatusBarCount = allyStatusBarCount+1 % allyStatusBars.Count;
				}

				/*
				newStatusBar = GameObject.Instantiate (statusBar,Vector3.zero,Quaternion.identity) as GameObject;
				newStatusBar.transform.position = new Vector3 (-19f, 10f) + new Vector3(0f,statusBarCount * 2f);
				newStatusBar.transform.localScale = new Vector3 (3f, 3f);
				newStatusBar.transform.SetParent(canvas);
				newStatusBar.GetComponent<StatusBar>().ship = newObject.GetComponent<ShipControl>();
				*/
			}
			//If the object is a jumpgate, set the exit system name
			if(newObject.GetComponent<JumpGate>() != null){

				//If this is a random encounter, there will be a jumpgate to cute space. Assign the current destination to it.
				if(scenePrefab.exitSystemName.ToLower() == "cutespace"){
					newObject.GetComponent<JumpGate>().exitSystemName = PersistentGameData.overworldDestinationName;
				}
				else{
					newObject.GetComponent<JumpGate>().exitSystemName = scenePrefab.exitSystemName;
				}


			}
			if(newObject.GetComponent<SpaceStation>() != null){
				newObject.GetComponent<SpaceStation>().npcName = scenePrefab.npcName;
				newObject.GetComponent<SpaceStation>().npcNameDisplay = scenePrefab.npcNameDisplay;
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
