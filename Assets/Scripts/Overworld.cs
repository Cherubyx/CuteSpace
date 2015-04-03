using UnityEngine;
using System.Collections;

public class Overworld : MonoBehaviour {

	public Texture2D cursorImage;
	
	private int cursorWidth = 24;
	private int cursorHeight = 24;

	Vector3 origin;
	Vector3 destination;
	public GameObject playerAvatar;

	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		origin = PersistentGameData.overworldOriginPosition;
		destination = PersistentGameData.overworldDestinationPosition;
		playerAvatar.transform.position = origin;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 dir = destination - playerAvatar.transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		angle -= 90f;
		playerAvatar.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		playerAvatar.transform.position = Vector3.MoveTowards(playerAvatar.transform.position,destination,0.5f * Time.deltaTime);
	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(Input.mousePosition.x - cursorWidth / 2, Screen.height - Input.mousePosition.y - cursorHeight / 2, cursorWidth, cursorHeight), cursorImage);
	}
}
