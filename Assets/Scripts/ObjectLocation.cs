using UnityEngine;
using System.Collections;

public class ObjectLocation{

	public string prefabName;
	public Vector3 position;
	public Quaternion rotation;
	public string owner;

	public ObjectLocation(string a_prefabName, Vector3 a_position, Quaternion a_rotation, string a_owner = "default"){
		prefabName = a_prefabName;
		position = a_position;
		rotation = a_rotation;
		owner = a_owner;
	}
}
