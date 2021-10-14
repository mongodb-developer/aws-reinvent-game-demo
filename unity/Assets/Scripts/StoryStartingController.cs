using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryStartingController : MonoBehaviour {

    public List<CharacterDialogue> dialogue;
    public Text instructionsText;

    private int _dialogueSetNumber = 0;

    void Awake() {
        StartCoroutine(StoryCoroutine());
        if(Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer) {
            instructionsText.gameObject.SetActive(true);
        }
    }

    void Start() {
        LevelManager.Instance.HideLoading();
    }

    IEnumerator StoryCoroutine() {
        yield return new WaitForSeconds(1);
        dialogue[_dialogueSetNumber].gameObject.SetActive(true);
        dialogue[_dialogueSetNumber].onDismiss.AddListener(DismissDialogue);
    }

    void DismissDialogue() {
        dialogue[_dialogueSetNumber].gameObject.SetActive(false);
        _dialogueSetNumber++;
        if(_dialogueSetNumber < dialogue.Count) {
            dialogue[_dialogueSetNumber].gameObject.SetActive(true);
            dialogue[_dialogueSetNumber].onDismiss.AddListener(DismissDialogue);
        } else {
            LevelManager.Instance.LoadScene("MidgardScene");
        }
    }

}