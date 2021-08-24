using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MNPC : MonoBehaviour {

    public string characterName;
    public Sprite characterIcon;
    public string storyMessage;
    public GameObject storyModal;
    public string scene;

    private bool _isStoryTelling = false;

    void Start() {
        
    }

    void Update() {
        if(_isStoryTelling == true && storyModal.activeInHierarchy == false) {
            if(scene != "") {
                SceneManager.LoadScene(scene);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Player") {
            _isStoryTelling = true;
            storyModal.GetComponent<MStoryModal>().storyMessage = storyMessage;
            storyModal.GetComponent<MStoryModal>().characterIcon = characterIcon;
            storyModal.SetActive(true);
        }
    }

}
