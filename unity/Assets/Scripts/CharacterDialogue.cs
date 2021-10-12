using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class CharacterDialogue : MonoBehaviour {

    public Sprite avatar;
    public List<string> messages;
    public UnityEvent onDismiss;
    
    [SerializeField] private Image _avatarImage;
    [SerializeField] private Text _messageText;

    private int _messageNumber = 0;

    void OnEnable() {
        _avatarImage.sprite = avatar;
        _messageText.text = messages[_messageNumber];
    }

    void Update() {
        if(Keyboard.current.enterKey.wasReleasedThisFrame || Pointer.current.press.wasPressedThisFrame) {
            if(_messageNumber < messages.Count - 1) {
                _messageNumber++;
                _messageText.text = messages[_messageNumber];
            } else {
                _messageNumber = 0;
                if(onDismiss != null) {
                    onDismiss.Invoke();
                }
                // gameObject.SetActive(false);
            }
        }
    }

}