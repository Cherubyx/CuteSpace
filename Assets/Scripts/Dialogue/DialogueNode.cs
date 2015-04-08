using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;

public class DialogueNode {

	[XmlAttribute("prompt")]
	public string prompt;

	[XmlAttribute("nodeID")]
	public int nodeID;

	[XmlArray("DialogueResponses")]
	[XmlArrayItem("DialogueResponse")]
	public List<DialogueResponse> dialogueResponses;

	/*
	string dialogueText;
    string alternateText;
	List<DialogueResponse> responseList;

    public bool IsNPCFriendly()
    {
        //if (player.Race != gameManager.instance.currentNPC.race) -> show alt Text
        return true;
    }
    */


}
