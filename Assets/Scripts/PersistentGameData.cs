using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistentGameData : MonoBehaviour {

	//Variables for overworld
	public static Vector3 overworldOriginPosition = new Vector3(-8.28f,4.36f,0f);
	public static string overworldDestinationName = "CanisMajor";

	//Variables for combat
	public static string playerShipName = "hornet";
	public static List<string> playerFleet = new List<string>();
	public static string combatSceneName = "default";

	//Variables for dialogue
	public static string npcName = "default";
    public static string traderSchnauz = "TraderSchnauz";
    public static string pirateGrumpy = "PirateGrumpy";

	//Other variables
	public static string playerRace = "Cat";
    public static string lastScene = "CuteSpace";
  
	//Faction hostility chart
	public enum factions {player,cat,dog,pirate,merchant,superhostile};

	//public static bool[,] factionAttitudes = bool[Enum.GetNames(typeof(factions)).Length,Enum.GetNames(typeof(factions)).Length];
	public static bool[,] factionEnemies = new bool[6,6] {/*player*/{false,true ,true ,true ,false,true},
														    /*cats*/{true ,false,true ,true ,false,true},
															/*dogs*/{true ,true ,false,true ,false,true},
														 /*pirates*/{true ,true ,true ,false,true ,true},
													   /*merchants*/{false,false,false,true ,false,true},
													/*superhostile*/{true ,true ,true ,true ,true ,true}};

	/**
	 * Trade-Related Values
	 */

  // The player's name.
  public static string playerName = "player";
  // The player's currency type.
  public static Currency playerCurrency = Currency.CHEEZBURGER;
  // The amount of currency the player has.
  public static int playerCoins = 100;
  // The player's inventory.
  public static List<Item> playerItems;

  // The trading partner's name.
  public static string partnerName;

}
