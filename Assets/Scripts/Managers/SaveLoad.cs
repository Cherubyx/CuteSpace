using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad : MonoBehaviour {

    public static List<GameData> savedGames = new List<GameData>();

    //adds current game to list of saved games
    public static void Save()
    {
        savedGames.Add(GameData.current);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, SaveLoad.savedGames);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            SaveLoad.savedGames = (List<GameData>)bf.Deserialize(file);
            file.Close();
        }
    }

    public static bool IsGameFilePresent()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd"))
        {
            return true;
        }
        return false;
    }
}
