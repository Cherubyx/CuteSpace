﻿using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

public class ScenePrefab{

	[XmlAttribute("prefabName")]
	public string prefabName;
	[XmlAttribute("x")]
	public float x;
	[XmlAttribute("y")]
	public float y;
	[XmlAttribute("faction")]
	public string faction;
	[XmlAttribute("exitSystemName")]
	public string exitSystemName;
	[XmlAttribute("npcName")]
	public string npcName;
    [XmlAttribute("npcNameDisplay")]
    public string npcNameDisplay;


	/*
	public ScenePrefab(string a_prefabName, Vector3 a_position, Quaternion a_rotation, string a_owner = "default"){
		prefabName = a_prefabName;
		position = a_position;
		rotation = a_rotation;
		owner = a_owner;
	}
	*/
}
