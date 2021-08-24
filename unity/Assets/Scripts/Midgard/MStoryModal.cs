using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MStoryModal : MonoBehaviour {

    public string storyMessage;
    public Sprite characterIcon;

    private Text _storyText;
    private SpriteRenderer _spriteRenderer;

    void OnEnable() {
        Time.timeScale = 0.0f;
    }

    void Start() {
        GameObject storyTextGameObject = transform.Find("StoryModalText").gameObject;
        GameObject characterImageGameObject = transform.Find("NPCImage").gameObject;
        _storyText = storyTextGameObject.GetComponent<Text>();
        _spriteRenderer = characterImageGameObject.GetComponent<SpriteRenderer>();
        _storyText.text = storyMessage;
        _spriteRenderer.sprite = characterIcon;
    }

    void Update() {
        if(Input.GetKey(KeyCode.Return)) {
            Time.timeScale = 1.0f;
            gameObject.SetActive(false);
        }
    }

}
