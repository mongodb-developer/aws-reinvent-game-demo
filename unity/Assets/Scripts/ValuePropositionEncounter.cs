using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEditor.Animations;

public class ValuePropositionEncounter : MonoBehaviour {

    [SerializeField] private GameObject _canvas;
    [SerializeField] private GameObject _sprite;
    [SerializeField] private AnimatorController _animatorController;
    [SerializeField] private Text _messageText;
    [SerializeField] private Sprite _spriteImage;
    [SerializeField] private string _message;
    [SerializeField] private bool _isFacingRight = true;
    
    void OnEnable() {
        Time.timeScale = 1.0f;
        _messageText.text = _message;
        _sprite.GetComponent<ValuePropositionSprite>().onTriggerEnter2D.AddListener(onTriggerEnter2D);
        _sprite.GetComponent<SpriteRenderer>().sprite = _spriteImage;
        if(_animatorController != null) {
            _sprite.GetComponent<Animator>().runtimeAnimatorController = _animatorController;
        } else {
            _sprite.GetComponent<Animator>().enabled = false;
        }
        if(_isFacingRight == false) {
            _sprite.transform.localScale = new Vector3(-1.0f * _sprite.transform.localScale.x, _sprite.transform.localScale.y, _sprite.transform.localScale.z);
        }
    }

    void Update() {
        if(_canvas.activeInHierarchy) {
            if(Keyboard.current.spaceKey.wasReleasedThisFrame) {
                _canvas.SetActive(false);
                _sprite.GetComponent<ValuePropositionSprite>().onTriggerEnter2D.RemoveListener(onTriggerEnter2D);
                Time.timeScale = 1.0f;
            }
        }
    }

    void onTriggerEnter2D() {
        Time.timeScale = 0.0f;
        _canvas.SetActive(true);
    }

}