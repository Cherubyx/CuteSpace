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
			PersistentGameData.npcName = npcName;
			GameObject.Find("Scene Curtain").GetComponent<SceneCurtain>().closeCurtain();
			StartCoroutine(WaitAndLoadLevel (2f,"Trade"));
		}
	}

	IEnumerator WaitAndLoadLevel(float waitTime, string levelName) {
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel(levelName);
	}
}
