using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour {

	public Texture2D cursorImage;

	private int cursorWidth = 32;
	private int cursorHeight = 32;

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
		Cursor.visible = false;
        //Debugging only
        LoadXMLDialogue("TraderToast");
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

        UpdateCoinAmount();
	}

    public void UpdateCoinAmount()
    {
        cheezBurgerAmount.text = PersistentGameData.cheezburgerCount.ToString();
        dogeCoinAmount.text = PersistentGameData.dogecoinCount.ToString();
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
        if (PersistentGameData.playerRace.ToLower() == "cat")
        {
            for (int i = 0; i < currentDialogue.dialogueTypes.Count; i++)
            {
                if (currentDialogue.dialogueTypes[i].Target.ToLower() == "cat")
                {
                    currentNode = currentDialogue.dialogueTypes[i].dialogueNodes[0];
                    currentDiagType = currentDialogue.dialogueTypes[i];
                }
            }
        }
        else if (PersistentGameData.playerRace.ToLower() == "dog")
        {
            for (int i = 0; i < currentDialogue.dialogueTypes.Count; i++)
            {
                if (currentDialogue.dialogueTypes[i].Target.ToLower() == "dog")
                {
                    currentNode = currentDialogue.dialogueTypes[i].dialogueNodes[0];
                    currentDiagType = currentDialogue.dialogueTypes[i];
                }
            }
        }
        else //default debug
        {
            currentDiagType = currentDialogue.dialogueTypes[0];
            currentNode = currentDiagType.dialogueNodes[0];
        }
	
		promptText.text = parseString(currentNode.prompt).ToUpper();

		foreach (Text text in responseTextList)
		{
			text.text = "";
		}
		
		for (int i = 0; i < currentNode.dialogueResponses.Count; i++)
		{
            Debug.Log("PARSING");
			responseTextList[i].text = "• " + parseString(currentNode.dialogueResponses[i].responseText).ToUpper();
		}
    }

	public void PickResponse(int index){

        // For coin spending
        if (currentNode.dialogueResponses[index].dogeCoins != null)
        {
            int coins = int.Parse(parseString(currentNode.dialogueResponses[index].dogeCoins.ToString()));
            PersistentGameData.dogecoinCount -= coins;
        }
        if (currentNode.dialogueResponses[index].cheezBurgers != null)
        {
            int coins = int.Parse(parseString(currentNode.dialogueResponses[index].cheezBurgers.ToString()));
            PersistentGameData.cheezburgerCount -= coins;
        }

        // For a set of conditions, highest priority is scene change
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
        // Check for enough money before prompting next node
        if (currentNode.dialogueResponses[index].condition != null)
        {
            if (currentNode.dialogueResponses[index].condition.ToLower() == "cheezburger")
            {
                // Absolutely need to have choiceOne and choiceTwo defined in the XML
                if (PersistentGameData.cheezburgerCount < 10)
                {
                    currentNode = currentDiagType.dialogueNodes.Find(n => n.nodeID == currentNode.dialogueResponses[index].choiceTwo);
                }
                else currentNode = currentDiagType.dialogueNodes.Find(n => n.nodeID == currentNode.dialogueResponses[index].choiceOne);
            }
            else if (currentNode.dialogueResponses[index].condition.ToLower() == "dogecoin")
            {
                // Absolutely need to have choiceOne and choiceTwo defined in the XML
                if (PersistentGameData.dogecoinCount < 10)
                {
                    currentNode = currentDiagType.dialogueNodes.Find(n => n.nodeID == currentNode.dialogueResponses[index].choiceTwo);
                }
                else currentNode = currentDiagType.dialogueNodes.Find(n => n.nodeID == currentNode.dialogueResponses[index].choiceOne);
            }
        }
        else  // Just display the next prompt text after choice
        {
            currentNode = currentDiagType.dialogueNodes.Find(n => n.nodeID == currentNode.dialogueResponses[index].targetNodeID);
        }
        
        promptText.text = parseString(currentNode.prompt).ToUpper();

        foreach (Text text in responseTextList)
        {
            text.text = "";
        }

        for (int i = 0; i < currentNode.dialogueResponses.Count; i++)
        {
            responseTextList[i].text = "• " + parseString(currentNode.dialogueResponses[i].responseText).ToUpper();
        }
        
	}

	string parseString(string temp){
        temp = temp.Replace("$dogecoin", PersistentGameData.dogecoinCount.ToString());
        temp = temp.Replace("$cheezburger", PersistentGameData.cheezburgerCount.ToString());
        temp = temp.Replace("$playerRace", PersistentGameData.playerRace);
        temp = temp.Replace("$playerShip", PersistentGameData.playerShipName);
        temp = temp.Replace("$dc%", ((10f / 100f) * PersistentGameData.dogecoinCount).ToString());
        temp = temp.Replace("$cb%", ((10f / 100f) * PersistentGameData.cheezburgerCount).ToString());
		return temp;
	}

	void OnGUI()
	{
		GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, cursorWidth, cursorHeight), cursorImage);
	}

}
