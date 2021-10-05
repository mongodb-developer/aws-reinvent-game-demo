using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IRPlayer : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public InfiniteRunnerController controller;

    void Start() {
        
    }

    void Update() {
        if(Keyboard.current.upArrowKey.isPressed && transform.position.y <= 4.25) {
            transform.position += Vector3.up * movementSpeed * Time.deltaTime;
        } else if(Keyboard.current.downArrowKey.isPressed && transform.position.y >= -4.25) {
            transform.position += Vector3.down * movementSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Obstacle") {
            controller.ShowGameOverModal();
        }
    }

}
