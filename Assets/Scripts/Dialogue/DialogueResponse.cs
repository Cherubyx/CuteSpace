using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using System.Collections;

public class DialogueResponse {

	[XmlAttribute("text")]
	string responseText;

	[XmlAttribute("targetNodeID")]
	int targetNodeID;
	
	//Conditions for response?
	//Triggers and flags?
}
