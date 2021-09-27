using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSPlayer : MonoBehaviour {

    private Rigidbody2D _rb2d;
    private bool _isGrounded = true;

    [Range(1, 10)]
    public float movementSpeed = 5.0f;

    [Range(1, 10)]
    public float jumpVelocity = 7.0f;

    [Range(1, 5)]
    public float fallingMultiplier = 3.0f;

    void Start() {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        if(Input.GetKey(KeyCode.LeftArrow)) {
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
        } else if(Input.GetKey(KeyCode.RightArrow)) {
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.Space) && _isGrounded == true) {
            _rb2d.velocity += Vector2.up * jumpVelocity;
            _isGrounded = false;
        }

        if((_rb2d.velocity.y < 0) || (_rb2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space))) {
            _rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallingMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.name == "Ground" || collision.collider.name == "Platforms") {
            _isGrounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Pineapple") {
            collider.gameObject.SetActive(false);
        }
    }

}