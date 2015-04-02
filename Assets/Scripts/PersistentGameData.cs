using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistentGameData : MonoBehaviour {

	public static Vector3 overworldOriginPosition = new Vector3(7.04f,0.83f,0f);
	public static Vector3 overworldDestinationPosition = new Vector3(3.64f, -4.21f,0f);
	public static string overworldDestinationName = "";

	public static List<string> playerFleet = new List<string>();
	public static List<string> enemyFleet = new List<string>();

	public static List<ObjectLocation> objectSpawnList = new List<ObjectLocation>();
}
