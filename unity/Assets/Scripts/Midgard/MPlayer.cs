using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MPlayer : MonoBehaviour {

    private Animator _animator;

    public float movementSpeed = 5.0f;

    void Awake() {
        if(RealmController.Instance != null) {
            transform.position = new Vector2((float)RealmController.Instance.GetCurrentPlayer().X, (float)RealmController.Instance.GetCurrentPlayer().Y);
        }
    }

    void Start() {
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        if(Input.GetKey(KeyCode.UpArrow)) {
            ResetAnimations();
            _animator.SetBool("doWalkUp", true);
            transform.position += Vector3.up * movementSpeed * Time.deltaTime;
        } else if(Input.GetKey(KeyCode.DownArrow)) {
            ResetAnimations();
            _animator.SetBool("doWalkDown", true);
            transform.position += Vector3.down * movementSpeed * Time.deltaTime;
        } else if(Input.GetKey(KeyCode.LeftArrow)) {
            ResetAnimations();
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            _animator.SetBool("doWalk", true);
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
        } else if(Input.GetKey(KeyCode.RightArrow)) {
            ResetAnimations();
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            _animator.SetBool("doWalk", true);
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        } else {
            ResetAnimations();
        }
        if(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) {
            if(RealmController.Instance != null) {
                RealmController.Instance.UpdatePositionInMidgard(transform.position.x, transform.position.y);
            }
        }
    }

    private void ResetAnimations() {
        _animator.SetBool("doWalk", false);
        _animator.SetBool("doWalkDown", false);
        _animator.SetBool("doWalkUp", false);
    }

}
