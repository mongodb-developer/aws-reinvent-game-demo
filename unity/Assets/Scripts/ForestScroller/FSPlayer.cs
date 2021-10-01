using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSPlayer : MonoBehaviour {

    private Rigidbody2D _rb2d;
    private Animator _animator;
    private bool _isGrounded = true;

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
    }

    void FixedUpdate() {
        if(Input.GetKey(KeyCode.LeftArrow)) {
            _animator.SetBool("doWalk", true);
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
        } else if(Input.GetKey(KeyCode.RightArrow)) {
            _animator.SetBool("doWalk", true);
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        } else {
            _animator.SetBool("doWalk", false);
        }

        if(Input.GetKey(KeyCode.Space) && _isGrounded == true) {
            _animator.SetBool("doJump", true);
            _rb2d.velocity += Vector2.up * jumpVelocity;
            _isGrounded = false;
        }

        if((_rb2d.velocity.y < 0) || (_rb2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space))) {
            _rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallingMultiplier - 1) * Time.fixedDeltaTime;
        }

        if(transform.position.y <= -13.0f) {
            controller.ShowGameOverModal();
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.name == "Ground" || collision.collider.name == "Platforms") {
            _animator.SetBool("doJump", false);
            _isGrounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Pineapple") {
            controller.IncreaseScore(1);
            collider.gameObject.SetActive(false);
        }
    }

}