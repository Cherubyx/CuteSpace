using UnityEngine;
using System.Collections;

public class SpaceStation : MonoBehaviour {

	public float rotationRate;
	public string npcName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(0f,0f,rotationRate);
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player"){
			PersistentGameData.lastScene = "Combat";
			PersistentGameData.npcName = npcName;
			PersistentGameData.partnerName = npcName;
			GameObject.Find("Scene Curtain").GetComponent<SceneCurtain>().closeCurtain();
			StartCoroutine(WaitAndLoadLevel (2f,"Trade"));
		}
	}

	IEnumerator WaitAndLoadLevel(float waitTime, string levelName) {
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel(levelName);
	}

	void OnGUI()
	{
		if(Camera.current != null){
			Vector2 bottomLeft = Camera.current.ScreenToWorldPoint(Vector2.zero);
			Vector2 topRight = Camera.current.ScreenToWorldPoint(new Vector2(Camera.current.pixelWidth, Camera.current.pixelHeight));
			Vector2 pos = transform.position;
			float boxwidth = npcName.Length * 7.8f; //hackity hack hack to get the length of the box
			pos.x = Mathf.Clamp(pos.x -1f, bottomLeft.x, topRight.x);
			pos.y = Mathf.Clamp(pos.y -1f, bottomLeft.y, topRight.y);
			pos = Camera.current.WorldToScreenPoint(pos);
			pos.x = Mathf.Clamp(pos.x, 0f, Screen.width-boxwidth);
			pos.y = Mathf.Clamp(pos.y, 22f, Screen.height);
			//GUI.DrawTexture(new Rect(pos.x, Screen.height - pos.y, 28f, 42f), pointerArrow);
			GUI.TextArea(new Rect(pos.x, Screen.height - pos.y, boxwidth,22f),npcName);
		}
	}


}
