using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPlayer : MonoBehaviour {

    public float movementSpeed = 5.0f;
    public GameObject fishHook;
    public FishingController controller;

    public bool _isCasting = false;

    private PlayerInput _playerInput;
    
    void Start() {
        _playerInput = GetComponent<PlayerInput>();
    }

    void Update() {
        Vector2 input = _playerInput.actions["Move"].ReadValue<Vector2>();
        if(_isCasting == false) {
            if(input.x < 0 && transform.position.x >= -9.25f) {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                transform.position += Vector3.left * movementSpeed * Time.deltaTime;
            } else if(input.x > 0 && transform.position.x <= 9.25f) {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                transform.position += Vector3.right * movementSpeed * Time.deltaTime;
            } else if(Keyboard.current.spaceKey.wasReleasedThisFrame) {
                _isCasting = true;
                fishHook.transform.position = new Vector3(transform.position.x, transform.position.y - 0.75f, 0.0f);
                fishHook.SetActive(true);
                fishHook.GetComponent<FHook>().DropLine();
            }
        } else {
            if(Keyboard.current.spaceKey.wasReleasedThisFrame) {
                fishHook.GetComponent<FHook>().ReelLine();
            }
            _isCasting = fishHook.activeInHierarchy;
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Fish") {
            int fishWeight = collider.gameObject.GetComponent<Fish>().fishWeight;
            collider.gameObject.SetActive(false);
            controller.IncreaseScore(fishWeight);
        }
    }

}