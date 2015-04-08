using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using System.Collections;

public class DialogueResponse {

	[XmlAttribute("text")]
	public string responseText;

	[XmlAttribute("loadScene")]
	public string sceneName;

	[XmlAttribute("targetNodeID")]
	public int targetNodeID;
	
	//Conditions for response?
	//Triggers and flags?
}
