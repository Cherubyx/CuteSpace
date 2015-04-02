using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour {

	public Texture2D cursorImage;
	
	private int cursorWidth = 32;
	private int cursorHeight = 32;

	int rewardCoins;
	// Use this for initialization
	void Start () {

		Cursor.visible = false;

		foreach(ObjectLocation objectLocation in PersistentGameData.objectSpawnList){
			Instantiate(objectLocation.gameObject,objectLocation.position,objectLocation.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(Input.mousePosition.x - cursorWidth / 2, Screen.height - Input.mousePosition.y - cursorHeight / 2, cursorWidth, cursorHeight), cursorImage);
	}
}
