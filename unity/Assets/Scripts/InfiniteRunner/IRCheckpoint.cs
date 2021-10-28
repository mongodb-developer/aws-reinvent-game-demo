using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRCheckpoint : MonoBehaviour {

    public float startTime = 5.0f;
    public float movementSpeed = 5.0f;

    private float _acceleration = 1.0f;

    void Awake() {
        transform.position = new Vector3(14.0f, -4.6f, 0.0f);
    }

    void Update() {
        if(Time.timeSinceLevelLoad >= startTime) {
            if((int) Time.timeSinceLevelLoad >= 50) {
                _acceleration = 2.50f;
            } else if((int) Time.timeSinceLevelLoad >= 40) {
                _acceleration = 2.25f;
            } else if((int) Time.timeSinceLevelLoad >= 30) {
                _acceleration = 1.75f;
            } else if((int) Time.timeSinceLevelLoad >= 20) {
                _acceleration = 1.50f;
            } else if((int) Time.timeSinceLevelLoad >= 10) {
                _acceleration = 1.25f;
            }
            transform.position += Vector3.left * movementSpeed * Time.deltaTime * _acceleration;
        }
        if(transform.position.x < -14.0) {
            gameObject.SetActive(false);
        }
    }

}