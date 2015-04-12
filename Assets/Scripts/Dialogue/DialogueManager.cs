﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour {

    private static DialogueManager sInstance;
    public static DialogueManager Instance
    {
        get { return sInstance; }
    }

	public Text promptText;
    public Text npcNameText;
    public Image npcAvatarImg;
	public List<Text> responseTextList;

	Dialogue currentDialogue;
	DialogueNode currentNode;

	void Start(){
        sInstance = this;

		TextAsset dialogueXML = Resources.Load(PersistentGameData.npcName+"_dialogue") as TextAsset;
		currentDialogue = Dialogue.LoadFromText (dialogueXML.ToString());
        npcNameText.text = currentDialogue.NpcName;
        //npcAvatarImg.mainTexture = ;

		currentNode = currentDialogue.dialogueNodes[0];
		promptText.text = currentNode.prompt.ToUpper();

		foreach(Text text in responseTextList){
			text.text = "";
		}

		for(int i = 0; i<currentNode.dialogueResponses.Count; i++){
			responseTextList[i].text = "• " + currentNode.dialogueResponses[i].responseText.ToUpper();
		}
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			pickResponse(0);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2) && currentNode.dialogueResponses.Count > 1){
			pickResponse(1);
		}
		if(Input.GetKeyDown(KeyCode.Alpha3) && currentNode.dialogueResponses.Count > 2){
			pickResponse(2);
		}
		if(Input.GetKeyDown(KeyCode.Alpha4) && currentNode.dialogueResponses.Count > 3){
			pickResponse(3);
		}
	}

	public void pickResponse(int index){

		if(currentNode.dialogueResponses[index].sceneName != null){
			Application.LoadLevel(currentNode.dialogueResponses[index].sceneName);
		}
		else{
			currentNode = currentDialogue.dialogueNodes.Find(n => n.nodeID == currentNode.dialogueResponses[index].targetNodeID);
			promptText.text = currentNode.prompt.ToUpper();
			
			foreach(Text text in responseTextList){
				text.text = "";
			}
			
			for(int i = 0; i<currentNode.dialogueResponses.Count; i++){
				responseTextList[i].text = "• " + currentNode.dialogueResponses[i].responseText.ToUpper();
			}
		}
	}

}
