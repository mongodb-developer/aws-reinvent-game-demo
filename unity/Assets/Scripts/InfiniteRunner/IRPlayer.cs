using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRPlayer : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public InfiniteRunnerController controller;

    void Start() {
        
    }

    void Update() {
        if(Input.GetKey(KeyCode.UpArrow) && transform.position.y <= 4.25) {
            transform.position += Vector3.up * movementSpeed * Time.deltaTime;
        } else if(Input.GetKey(KeyCode.DownArrow) && transform.position.y >= -4.25) {
            transform.position += Vector3.down * movementSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Obstacle") {
            controller.ShowGameOverModal();
        }
    }

}
