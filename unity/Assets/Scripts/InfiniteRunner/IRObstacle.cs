using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRObstacle : MonoBehaviour {

    public float movementSpeed;

    private float[] _fixedPositionY = new float[] { -3.75f, 0.0f, 3.75f };
    private float _acceleration = 1.0f;

    void OnEnable() {
        int randomPositionY = Random.Range(0, 3);
        transform.position = new Vector3(14.0f, _fixedPositionY[randomPositionY], 0.0f);
    }

    void Update() {
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
        if(transform.position.x < -14.0) {
            gameObject.SetActive(false);
        }
    }

}