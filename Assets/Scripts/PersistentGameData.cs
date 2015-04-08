using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistentGameData : MonoBehaviour {

	//Variables for overworld
	public static Vector3 overworldOriginPosition = new Vector3(7.04f,0.83f,0f);
	public static Vector3 overworldDestinationPosition = new Vector3(3.64f, -4.21f,0f);
	public static string overworldDestinationName = "";

	//Variables for combat
	public static string playerShipName = "Hornet";
	public static List<string> playerFleet = new List<string>();
	public static List<string> enemyFleet = new List<string>();
	public static List<ObjectLocation> objectSpawnList = new List<ObjectLocation>();

	//Variables for dialogue
	public static string npcName = "default";

	//Variables for trade
	//player inventory
	//ALAN PLZ ADD
}
