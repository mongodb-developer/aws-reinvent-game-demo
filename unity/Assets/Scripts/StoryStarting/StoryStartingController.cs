using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStartingController : MonoBehaviour {

    public List<CharacterDialogue> dialogue;

    // private List<GameObject> _dialogue;

    private int _dialogueSetNumber = 0;

    void Awake() {
        dialogue[_dialogueSetNumber].gameObject.SetActive(true);
        dialogue[_dialogueSetNumber].onDismiss.AddListener(DismissDialogue);
        // dialogue[_dialogueSetNumber].SetActive(true);
        // _dialogue = new List<GameObject>();
        // _dialogBox.GetComponent<DialogueScript>().messages = new List<string>();
        // _dialogBox.GetComponent<DialogueScript>().messages.Add("This is a Test");
        // _dialogBox.GetComponent<DialogueScript>().onDismiss.AddListener(DismissDialogue);
    }

    void Start() {
        // GameObject tmp = Instantiate(_dialogBox);

    }

    void Update() {

    }

    void DismissDialogue() {
        dialogue[_dialogueSetNumber].gameObject.SetActive(false);
        _dialogueSetNumber++;
        if(_dialogueSetNumber < dialogue.Count) {
            dialogue[_dialogueSetNumber].gameObject.SetActive(true);
            dialogue[_dialogueSetNumber].onDismiss.AddListener(DismissDialogue);
        }
        // Debug.Log("TEST");
        // Destroy(_dialogBox);
    }

}
