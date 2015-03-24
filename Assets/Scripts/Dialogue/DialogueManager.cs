using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour {

    public List<DialogueNode> dnodes;

    private DialogueManager sInstance;
    public DialogueManager Instance
    {
        get { return sInstance; }
    }

    void Start()
    {
        sInstance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Need to have an idea of the previous scene (to return to if not changing to a different)
    void Update()
    {
    }


}
