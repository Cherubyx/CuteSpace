using UnityEngine;
using System.Collections;

public class MusicTest : MonoBehaviour {

	void Start () {
        MusicManager.Instance.Play("Main");
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            MusicManager.Instance.Play("Pew");
        }
	}
}
