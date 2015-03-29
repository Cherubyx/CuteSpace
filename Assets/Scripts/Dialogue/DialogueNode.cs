using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueNode {
	string dialogueText;
    string alternateText;
	List<DialogueResponse> responseList;

    public bool IsNPCFriendly()
    {
        //if (player.Race != gameManager.instance.currentNPC.race) -> show alt Text
        return true;
    }


}
