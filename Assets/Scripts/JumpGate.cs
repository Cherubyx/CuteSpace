using UnityEngine;
using System.Collections;

public class JumpGate : MonoBehaviour {

	public string exitSystemName;
	public Texture2D pointerArrow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
		PersistentGameData.overworldDestinationName = exitSystemName;
		StartCoroutine(WaitFor(2.0f));
		Application.LoadLevel("Overworld");
		}
	}

	IEnumerator WaitFor(float waitTime) {
		yield return new WaitForSeconds(waitTime);
	}

	void OnGUI()
	{
		if(Camera.current != null){
			Vector2 bottomLeft = Camera.current.ScreenToWorldPoint(Vector2.zero);
			Vector2 topRight = Camera.current.ScreenToWorldPoint(new Vector2(Camera.current.pixelWidth, Camera.current.pixelHeight));
			Vector2 pos = transform.position;
			pos.x = Mathf.Clamp(pos.x -1f, bottomLeft.x, topRight.x);
			pos.y = Mathf.Clamp(pos.y -1f, bottomLeft.y, topRight.y);
			pos = Camera.current.WorldToScreenPoint(pos);
			//GUI.DrawTexture(new Rect(pos.x, Screen.height - pos.y, 28f, 42f), pointerArrow);
			GUI.TextArea(new Rect(pos.x, Screen.height - pos.y, 150f,50f),"Jumpgate to "+exitSystemName);
		}
	}
}
