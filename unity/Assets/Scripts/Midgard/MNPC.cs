using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MNPC : MonoBehaviour {

    public string characterName;
    public Sprite characterIcon;
    public List<string> storyMessages;
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
        if(collider.tag == "Player" && Time.timeSinceLevelLoad > 1.0f) {
            _isStoryTelling = true;
            storyModal.GetComponent<MStoryModal>().storyMessages = storyMessages;
            storyModal.GetComponent<MStoryModal>().characterIcon = characterIcon;
            storyModal.SetActive(true);
        }
    }

}
