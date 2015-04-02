using UnityEngine;
using System.Collections;

public class ObjectLocation{

	public GameObject gameObject;
	public Vector3 position;
	public Quaternion rotation;
	public string owner;

	public ObjectLocation(GameObject a_object, Vector3 a_position, Quaternion a_rotation, string a_owner = "default"){
		gameObject = a_object;
		position = a_position;
		rotation = a_rotation;
		owner = a_owner;
	}
}
