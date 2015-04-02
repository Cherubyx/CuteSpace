using UnityEngine;
using System.Collections;

public class Overworld : MonoBehaviour {

	Vector3 origin;
	Vector3 destination;
	public GameObject playerAvatar;

	// Use this for initialization
	void Start () {
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
}
