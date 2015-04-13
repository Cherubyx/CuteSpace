using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public enum MainMenuState
    {
        MainMenu,
        NewGame,
        Continue
    }
    public enum Scenes
    {
        CuteSpace,
        Combat,
        Dialogue,
        Overworld,
        Trade
    }

    #region Variables
    public MainMenuState menuState;
    public Scenes currentScene;

    public GameObject NewGameButton;
    public GameObject CreditsButton;
    public GameObject QuitButton;

    public GameObject CreditsPanel;
    public GameObject CreditsScroll;
    public GameObject ReturnButton;

    public Texture2D cursorImage;

    private int cursorWidth = 32;
    private int cursorHeight = 32;

    private GameObject LoadGameButton;
    #endregion

    #region Properties
    private static GameManager sInstance;
	public static GameManager Instance
    {
		get { return sInstance; }
	}
    #endregion

    void Start()
	{
		sInstance = this;
        DontDestroyOnLoad(this.gameObject);
        menuState = MainMenuState.MainMenu;
        currentScene = Scenes.CuteSpace;
        Cursor.visible = false;
	}

	// Update is called once per frame
	void Update () {

        if (currentScene == Scenes.CuteSpace && menuState == MainMenuState.NewGame)
        {
            NewGame();
        }

	}

    void OnGUI()
    {
        if (currentScene == Scenes.CuteSpace)
        {
            GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, cursorWidth, cursorHeight), cursorImage);
        }
    }

    public void NewGame()
    {
        //SaveLoad.Save();
        menuState = MainMenuState.NewGame;
		currentScene = Scenes.Overworld;

		/*
		PersistentGameData.objectSpawnList = new List<ObjectLocation>();
		PersistentGameData.objectSpawnList.Add(new ObjectLocation("300i",new Vector3(0f,0f,0f),Quaternion.identity,"Player"));
		PersistentGameData.objectSpawnList.Add(new ObjectLocation("pirate",new Vector3(10f,0f,0f),Quaternion.identity,"Team2"));
		PersistentGameData.objectSpawnList.Add(new ObjectLocation("pirate",new Vector3(-10f,0f,0f),Quaternion.identity,"Team2"));
		PersistentGameData.objectSpawnList.Add(new ObjectLocation("pirate",new Vector3(0f,7f,0f),Quaternion.identity,"Team2"));
		PersistentGameData.objectSpawnList.Add(new ObjectLocation("pirate",new Vector3(0f,-7f,0f),Quaternion.identity,"Team2"));
		*/

		/*
		PersistentGameData.objectSpawnList = new List<ObjectLocation>();
		PersistentGameData.objectSpawnList.Add(new ObjectLocation("300i",new Vector3(0f,0f,0f),Quaternion.identity,"Player"));
		PersistentGameData.objectSpawnList.Add(new ObjectLocation("pirate",new Vector3(10f,0f,0f),Quaternion.identity,"Team2"));
		PersistentGameData.objectSpawnList.Add(new ObjectLocation("pirate",new Vector3(-10f,0f,0f),Quaternion.identity,"Team2"));
		PersistentGameData.objectSpawnList.Add(new ObjectLocation("pirate",new Vector3(0f,7f,0f),Quaternion.identity,"Team2"));
		PersistentGameData.objectSpawnList.Add(new ObjectLocation("pirate",new Vector3(0f,-7f,0f),Quaternion.identity,"Team2"));
		*/

		Application.LoadLevel("RaceSelection");
    }

    public void LoadGame()
    {
        //SaveLoad.Load();
        menuState = MainMenuState.Continue;
    }

    public void Credits()
    {
        NewGameButton.SetActive(false);
        CreditsButton.SetActive(false);
        QuitButton.SetActive(false);

        CreditsPanel.SetActive(true);
        CreditsScroll.SetActive(true);
        ReturnButton.SetActive(true);
    }

    public void Return()
    {
        NewGameButton.SetActive(true);
        CreditsButton.SetActive(true);
        QuitButton.SetActive(true);

        CreditsPanel.SetActive(false);
        CreditsScroll.SetActive(false);
        ReturnButton.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
