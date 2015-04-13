using UnityEngine;
using System.Collections;

public class OverworldNPCAvatar : MonoBehaviour {

	private GameObject playerAvatar;
	public string npcName = "ShadyShibe";

	// Use this for initialization
	void Start () {
		playerAvatar = GameObject.Find ("OverworldPlayerAvatar");
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 dir = playerAvatar.transform.position - transform.position;
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
		angle -= 90f;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.position = Vector2.MoveTowards(transform.position,playerAvatar.transform.position,1.0f * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject == playerAvatar){
			PersistentGameData.overworldOriginPosition = this.transform.position;
			PersistentGameData.npcName = this.npcName;
			GameObject.Find("Scene Curtain").GetComponent<SceneCurtain>().closeCurtain();
			StartCoroutine(WaitAndLoadLevel(2,"Dialogue"));
		}
	}

	IEnumerator WaitAndLoadLevel(float waitTime, string levelName) {
        PersistentGameData.lastScene = "Overworld";
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel(levelName);
	}
}
