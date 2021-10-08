using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class MPlayer : MonoBehaviour {

    private Animator _animator;
    private PlayerInput _playerInput;
    private float _timeUntilPositionSave = 0.0f;

    public float movementSpeed = 5.0f;
    public float positionSaveTimeout = 2.0f;

    void Awake() {
        if(RealmController.Instance != null) {
            transform.position = new Vector2((float)RealmController.Instance.GetCurrentPlayer().X, (float)RealmController.Instance.GetCurrentPlayer().Y);
        }
    }

    void Start() {
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();
    }

    void FixedUpdate() {
        Vector2 input = _playerInput.actions["Move"].ReadValue<Vector2>();
        if(input != new Vector2(0, 0)) {
            transform.position += new Vector3(input.x, input.y, 0) * movementSpeed * Time.deltaTime;
            if(input == new Vector2(0f, 1f)) {
                ResetAnimations();
                _animator.SetBool("doWalkUp", true);
            } else if(input == new Vector2(0f, -1f)) {
                ResetAnimations();
                _animator.SetBool("doWalkDown", true);
            } else if(input.x < 0) {
                ResetAnimations();
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                _animator.SetBool("doWalk", true);
            } else if(input.x > 0) {
                ResetAnimations();
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                _animator.SetBool("doWalk", true);
            }
        } else {
            ResetAnimations();
        }
        _timeUntilPositionSave -= Time.deltaTime;
        if(_timeUntilPositionSave <= 0) {
            if(RealmController.Instance != null) {
                RealmController.Instance.UpdatePositionInMidgard(transform.position.x, transform.position.y);
            }
            _timeUntilPositionSave = positionSaveTimeout;
        }
        // if(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) {
        //     if(RealmController.Instance != null) {
        //         RealmController.Instance.UpdatePositionInMidgard(transform.position.x, transform.position.y);
        //     }
        // }
    }

    private void ResetAnimations() {
        _animator.SetBool("doWalk", false);
        _animator.SetBool("doWalkDown", false);
        _animator.SetBool("doWalkUp", false);
    }

}
