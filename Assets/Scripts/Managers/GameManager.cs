using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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

            GameData.current.gameName = "";
            //SaveLoad.Save();
            //LoadGame!
        }


	}
    public void NewGame()
    {
        //SaveLoad.Save();
        menuState = MainMenuState.NewGame;
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
