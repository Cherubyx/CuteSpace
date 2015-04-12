using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistentGameData : MonoBehaviour {

	//Variables for overworld
	public static Vector3 overworldOriginPosition = new Vector3(-8.28f,4.36f,0f);
	public static string overworldDestinationName = "CatStar1";

	//Variables for combat
	public static string playerShipName = "hornet";
	public static List<string> playerFleet = new List<string>();
	public static string combatSceneName = "default";

	//Variables for dialogue
	public static string npcName = "default";

	//Other variables
	public static string playerRace = "default";

	//Variables for trade
	//player inventory
	//ALAN PLZ ADD

	//Faction hostility chart
	public enum factions {player,cat,dog,pirate,merchant,superhostile};
	//public static bool[,] factionAttitudes = bool[Enum.GetNames(typeof(factions)).Length,Enum.GetNames(typeof(factions)).Length];
	public static bool[,] factionEnemies = new bool[6,6] {/*player*/{false,true ,true ,true ,false,true},
														    /*cats*/{true ,false,true ,true ,false,true},
															/*dogs*/{true ,true ,false,true ,false,true},
														 /*pirates*/{true ,true ,true ,false,true ,true},
													   /*merchants*/{false,false,false,true ,false,true},
													/*superhostile*/{true ,true ,true ,true ,true ,true}};				
}
