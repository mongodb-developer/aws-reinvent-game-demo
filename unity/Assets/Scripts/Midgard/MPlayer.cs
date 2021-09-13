using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MPlayer : MonoBehaviour {

    public float movementSpeed = 5.0f;

    void Awake() {
        if(RealmController.Instance != null) {
            transform.position = new Vector2((float)RealmController.Instance.GetCurrentPlayer().X, (float)RealmController.Instance.GetCurrentPlayer().Y);
        }
    }

    void Start() { }

    void FixedUpdate() {
        if(Input.GetKey(KeyCode.UpArrow)) {
            transform.position += Vector3.up * movementSpeed * Time.deltaTime;
        } else if(Input.GetKey(KeyCode.DownArrow)) {
            transform.position += Vector3.down * movementSpeed * Time.deltaTime;
        } else if(Input.GetKey(KeyCode.LeftArrow)) {
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
        } else if(Input.GetKey(KeyCode.RightArrow)) {
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        }
        if(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) {
            if(RealmController.Instance != null) {
                RealmController.Instance.UpdatePositionInMidgard(transform.position.x, transform.position.y);
            }
        }
    }

}
