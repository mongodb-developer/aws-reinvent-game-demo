using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRObstacle : MonoBehaviour {

    public float movementSpeed;

    private float[] _fixedPositionY = new float[] { -3.25f, 0.0f, 3.25f };

    void OnEnable() {
        int randomPositionY = Random.Range(0, 3);
        transform.position = new Vector3(10.0f, _fixedPositionY[randomPositionY], 0.0f);
    }

    void Update() {
        transform.position += Vector3.left * movementSpeed * Time.deltaTime;
        if(transform.position.x < -10.0) {
            gameObject.SetActive(false);
        }
    }

}