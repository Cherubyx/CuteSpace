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
	}
	

	// Update is called once per frame
	void Update () {

        //MainMenu
        if (currentScene == Scenes.CuteSpace && menuState == MainMenuState.MainMenu)
        {
            LoadGameButton = GameObject.Find("LoadGameButton");
            if (!SaveLoad.IsGameFilePresent())
            {
                LoadGameButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                LoadGameButton.GetComponent<Button>().interactable = true;
            }
        }

        if (currentScene == Scenes.CuteSpace && menuState == MainMenuState.Continue)
        {
            //Change canvas to look different

            foreach (GameData g in SaveLoad.savedGames)
            {
                //display each gamedata gamename
            }
        }

        if (currentScene == Scenes.CuteSpace && menuState == MainMenuState.NewGame)
        {
            //Change canvas look to name gamedata

			NewGame ();
            //GameData.current.gameName = "";
            //SaveLoad.Save();
            //LoadGame!
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
