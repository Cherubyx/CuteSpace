using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

    public void Return()
    {
        GameObject gameMger = GameObject.Find("GameManager");
        if (gameMger != null) Destroy(gameMger.gameObject);
        GameObject musicMger = GameObject.Find("MusicManager");
        if (musicMger != null) Destroy(musicMger.gameObject);
        Application.LoadLevel("CuteSpace");
    }
}
