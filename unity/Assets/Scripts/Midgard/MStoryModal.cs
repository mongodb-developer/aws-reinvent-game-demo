using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MStoryModal : MonoBehaviour {

    public Sprite characterIcon;
    public List<string> storyMessages;

    private Text _storyText;
    private SpriteRenderer _spriteRenderer;
    private int _messageNumber = 0;
    private GameObject _virtualJoyStick;

    void OnEnable() {
        Time.timeScale = 0.0f;
        GameObject storyTextGameObject = transform.Find("StoryModalText").gameObject;
        GameObject characterImageGameObject = transform.Find("NPCImage").gameObject;
        _storyText = storyTextGameObject.GetComponent<Text>();
        _spriteRenderer = characterImageGameObject.GetComponent<SpriteRenderer>();
        _storyText.text = storyMessages[_messageNumber];
        _spriteRenderer.sprite = characterIcon;
        _virtualJoyStick = transform.parent.Find("VirtualJoyStick").gameObject;
        _virtualJoyStick.SetActive(false);
    }

    void Update() {
        if(Keyboard.current.enterKey.wasReleasedThisFrame || Keyboard.current.spaceKey.wasReleasedThisFrame || Pointer.current.press.wasPressedThisFrame) {
            if(_messageNumber < storyMessages.Count - 1) {
                _messageNumber++;
                _storyText.text = storyMessages[_messageNumber];
            } else {
                _messageNumber = 0;
                Time.timeScale = 1.0f;
                if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
                    _virtualJoyStick.SetActive(true);
                }
                gameObject.SetActive(false);
            }
        }
    }

}
