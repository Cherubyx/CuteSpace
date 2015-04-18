using UnityEngine;
using System.Collections;

public class CateInfo : MonoBehaviour {

	public Texture2D cursorImage;
	
	private int cursorWidth = 32;
	private int cursorHeight = 32;
	
	// Use this for initialization
	void Start () {
		Cursor.visible = false;
	}
	
	// Use this for initialization
	public void nextScene () {
		StartCoroutine(WaitAndLoadLevel(0.5f, "Overworld"));
	}
	
	IEnumerator WaitAndLoadLevel(float waitTime, string levelName) {
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel(levelName);
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, cursorWidth, cursorHeight), cursorImage);
	}
}
