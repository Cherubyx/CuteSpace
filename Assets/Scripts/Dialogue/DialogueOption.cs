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
		this.GetComponent<Text>().color = new Color(0.1176f, 0.655f, 0.8824f);
	}

	void OnMouseDown() {
		dialogueManager.PickResponse(optionNumber);
	}
}
