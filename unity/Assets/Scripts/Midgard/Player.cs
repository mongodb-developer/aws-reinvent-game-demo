using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public float movementSpeed = 5.0f;

    void Start() {
        
    }

    void Update() {
        if(Input.GetKey(KeyCode.UpArrow)) {
            transform.position += Vector3.up * movementSpeed * Time.deltaTime;
        } else if(Input.GetKey(KeyCode.DownArrow)) {
            transform.position += Vector3.down * movementSpeed * Time.deltaTime;
        } else if(Input.GetKey(KeyCode.LeftArrow)) {
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
        } else if(Input.GetKey(KeyCode.RightArrow)) {
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "NPC") {
            SceneManager.LoadScene("InfiniteRunnerScene");
        }
    }

}
