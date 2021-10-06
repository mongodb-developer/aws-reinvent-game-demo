using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class FSPlayer : MonoBehaviour {

    private Rigidbody2D _rb2d;
    private Animator _animator;
    private bool _isGrounded = true;
    private PlayerInput _playerInput;

    public ForestScrollerController controller;

    [Range(1, 10)]
    public float movementSpeed = 5.0f;

    [Range(1, 10)]
    public float jumpVelocity = 7.0f;

    [Range(1, 5)]
    public float fallingMultiplier = 3.0f;

    void Start() {
        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<PlayerInput>();
    }

    void FixedUpdate() {
        Vector2 input = _playerInput.actions["Move"].ReadValue<Vector2>();
        if(input.x < 0) {
            _animator.SetBool("doWalk", true);
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
        } else if(input.x > 0) {
            _animator.SetBool("doWalk", true);
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        } else {
            _animator.SetBool("doWalk", false);
        }
        if(_playerInput.actions["Special"].ReadValue<float>() > 0 && _isGrounded == true) {
            _animator.SetBool("doJump", true);
            _rb2d.velocity += Vector2.up * jumpVelocity;
            _isGrounded = false;
        }

        if((_rb2d.velocity.y < 0) || (_rb2d.velocity.y > 0 && _playerInput.actions["Special"].ReadValue<float>() == 0)) {
            _rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallingMultiplier - 1) * Time.fixedDeltaTime;
        }

        if(transform.position.y <= -13.0f) {
            controller.ShowGameOverModal();
        }
    }

    void OnCollisionStay2D(Collision2D collision) {
        if(collision.collider.name == "Ground" || collision.collider.name == "Platforms") {
            foreach(ContactPoint2D contactPoint in collision.contacts) {
                if(Math.Round(contactPoint.normal.x) != 0) {
                    _isGrounded = false;
                } else if(Math.Round(contactPoint.normal.y) >= 1 && _rb2d.velocity.y <= 0) {
                    _animator.SetBool("doJump", false);
                    _isGrounded = true;
                    return;
                } else {
                    _isGrounded = false;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.tag == "Obstacle") {
            controller.ShowGameOverModal();
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Pineapple") {
            controller.IncreaseScore(1);
            collider.gameObject.SetActive(false);
        } else if(collider.gameObject.tag == "Trophy") {
            controller.ShowGameSuccessModal();
        }
    }

}