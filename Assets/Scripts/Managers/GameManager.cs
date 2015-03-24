using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private static GameManager sInstance;
	public static GameManager Instance
    {
		get { return sInstance; }
	}

	void Awake()
	{
		sInstance = this;
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NewGame()
    {

    }

    public void LoadGame()
    {

    }
}
