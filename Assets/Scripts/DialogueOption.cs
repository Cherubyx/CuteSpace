using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueOption : MonoBehaviour {

	public int optionNumber;
	public DialogueManager dialogueManager;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver() {
		this.GetComponent<Text>().color = new Color(1.0f,1.0f,1.0f);
	}

	void OnMouseExit() {
		this.GetComponent<Text>().color = new Color(0.784f,0.784f,0.784f);
	}

	void OnMouseDown() {
		dialogueManager.pickResponse(optionNumber);
	}
}
