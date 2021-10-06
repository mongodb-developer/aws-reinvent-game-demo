using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IRPlayer : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public InfiniteRunnerController controller;

    private PlayerInput _playerInput;

    void Start() {
        _playerInput = GetComponent<PlayerInput>();
    }

    void Update() {
        Vector2 input = _playerInput.actions["Move"].ReadValue<Vector2>();
        if(input.y > 0 && transform.position.y <= 4.25) {
            transform.position += Vector3.up * movementSpeed * Time.deltaTime;
        } else if(input.y < 0 && transform.position.y >= -4.25) {
            transform.position += Vector3.down * movementSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Obstacle") {
            controller.ShowGameOverModal();
        }
    }

}
