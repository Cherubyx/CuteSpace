using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

    public Texture2D cursorImage;

    private int cursorWidth = 32;
    private int cursorHeight = 32;

    void Start()
    {
        Cursor.visible = false;
    }

    public void Return()
    {
        GameObject gameMger = GameObject.Find("GameManager");
        if (gameMger != null) Destroy(gameMger.gameObject);
        GameObject musicMger = GameObject.Find("MusicManager");
        if (musicMger != null) Destroy(musicMger.gameObject);
        Application.LoadLevel("CuteSpace");
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, cursorWidth, cursorHeight), cursorImage);
    }

}
