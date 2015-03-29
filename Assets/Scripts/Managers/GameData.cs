using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameData : MonoBehaviour {

    public static GameData current; //game save state

    //Other classes or objects variables you want to persist between scenes  NONSTATIC
    public string gameName = "";
    //public List<Ship> shipInventory;
    //public List<Money> wallet;
    //public List<Items> inventory;
    public bool firstGameLoad;
    //more flags /conditions
    public int currentPositionInMap;

    public GameData()
    {
        gameName = "";
        firstGameLoad = true;
        currentPositionInMap = 0;
    }

    public void LoadData()
    {
    }

    public void SaveData()
    {

    }
}
