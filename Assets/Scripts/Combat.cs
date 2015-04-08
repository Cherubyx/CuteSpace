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
		foreach(ObjectLocation objectLocation in PersistentGameData.objectSpawnList){
			GameObject newObject = Instantiate(pfd.getPrefab(objectLocation.prefabName),objectLocation.position,objectLocation.rotation) as GameObject;
			newObject.tag = objectLocation.owner;
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
