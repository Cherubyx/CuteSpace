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
		PrefabDictionary pfd = GameObject.Find("PrefabDictionary").GetComponent<PrefabDictionary>();
		GameObject playerShip = pfd.instantiatePrefab(PersistentGameData.playerShipName,Vector3.zero,Quaternion.identity);
		GameObject.Find("Status Bar").GetComponent<StatusBar>().ship = playerShip.GetComponent<ShipControl>();

		TextAsset combatXML = Resources.Load(PersistentGameData.combatSceneName + "_combat") as TextAsset;
		ScenePrefabCollection scenePrefabCollection = ScenePrefabCollection.LoadFromText(combatXML.ToString());

		foreach(ScenePrefab scenePrefab in scenePrefabCollection.scenePrefabs){
			GameObject newObject = Instantiate(pfd.getPrefab(scenePrefab.prefabName),new Vector2(scenePrefab.x,scenePrefab.y),Quaternion.identity) as GameObject;
			newObject.tag = scenePrefab.owner;
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
