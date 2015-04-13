using UnityEngine;
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

    public Text cheezBurgerAmount;
    public Text dogeCoinAmount;

	Dialogue currentDialogue;
	DialogueNode currentNode;
    DialogueType currentDiagType;

    void Awake()
    {
        sInstance = this;
    }

    void Start()
    {
        //Debugging only
        LoadXMLDialogue("TraderSchnauz");

        cheezBurgerAmount = PersistentGameData.playerCoins;

    }

	void Update(){
		if(Input.GetKeyDown(KeyCode.Alpha1)){
			PickResponse(0);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2) && currentNode.dialogueResponses.Count > 1){
			PickResponse(1);
		}
		if(Input.GetKeyDown(KeyCode.Alpha3) && currentNode.dialogueResponses.Count > 2){
			PickResponse(2);
		}
		if(Input.GetKeyDown(KeyCode.Alpha4) && currentNode.dialogueResponses.Count > 3){
			PickResponse(3);
		}
	}

    public void LoadXMLDialogue(string npcName)
    {
        TextAsset dialogueXML = Resources.Load(npcName + "_dialogue") as TextAsset;
        currentDialogue = Dialogue.LoadFromText(dialogueXML.ToString());
        npcNameText.text = currentDialogue.NpcName.ToUpper();
        npcAvatarImg.sprite = (Sprite)Resources.Load("Sprites/"+npcName, typeof(Sprite));

        ChooseDialogueType();
    }

    public void ChooseDialogueType()
    {
        if (PersistentGameData.playerRace == "cat")
        {
            for (int i = 0; i < currentDialogue.dialogueTypes.Count; i++)
            {
                if (currentDialogue.dialogueTypes[i].Target == "Cat")
                {
                    currentNode = currentDialogue.dialogueTypes[i].dialogueNodes[0];
                    currentDiagType = currentDialogue.dialogueTypes[i];
                }
            }
            promptText.text = currentNode.prompt.ToUpper();

            foreach (Text text in responseTextList)
            {
                text.text = "";
            }

            for (int i = 0; i < currentNode.dialogueResponses.Count; i++)
            {
                responseTextList[i].text = "• " + currentNode.dialogueResponses[i].responseText.ToUpper();
            }
        }
        else if (PersistentGameData.playerRace == "dog")
        {
            for (int i = 0; i < currentDialogue.dialogueTypes.Count; i++)
            {
                if (currentDialogue.dialogueTypes[i].Target == "Dog")
                {
                    currentNode = currentDialogue.dialogueTypes[i].dialogueNodes[0];
                    currentDiagType = currentDialogue.dialogueTypes[i];
                }
            }
            promptText.text = currentNode.prompt.ToUpper();

            foreach (Text text in responseTextList)
            {
                text.text = "";
            }

            for (int i = 0; i < currentNode.dialogueResponses.Count; i++)
            {
                responseTextList[i].text = "• " + currentNode.dialogueResponses[i].responseText.ToUpper();
            }
        }
        else //default debug
        {
            currentDiagType = currentDialogue.dialogueTypes[0];
            currentNode = currentDiagType.dialogueNodes[0];
            promptText.text = currentNode.prompt.ToUpper();

            foreach (Text text in responseTextList)
            {
                text.text = "";
            }

            for (int i = 0; i < currentNode.dialogueResponses.Count; i++)
            {
                responseTextList[i].text = "• " + currentNode.dialogueResponses[i].responseText.ToUpper();
            }
        }
    }


	public void PickResponse(int index){

		if(currentNode.dialogueResponses[index].sceneName != null){
            if (currentNode.dialogueResponses[index].sceneName == "Previous")
            {
                Application.LoadLevel(PersistentGameData.lastScene);
            }
            else
            {
                Application.LoadLevel(currentNode.dialogueResponses[index].sceneName);
            }
		}
		else{
			currentNode = currentDiagType.dialogueNodes.Find(n => n.nodeID == currentNode.dialogueResponses[index].targetNodeID);
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
