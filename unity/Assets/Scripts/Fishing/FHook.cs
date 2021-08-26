using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FHook : MonoBehaviour {

    private bool _isDropLine = false;
    private bool _isReelLine = false;
    private Vector3 _startingPosition;
    private bool _hasHooked = false;

    void OnEnable() {
        _startingPosition = transform.position;
        _hasHooked = false;
    }

    void Update() {
        if(_isDropLine == true) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, -1.5f), 2.0f * Time.deltaTime);
            if(transform.position == new Vector3(transform.position.x, -1.5f, 0.0f)) {
                _isDropLine = false;
            }
        } else if(_isReelLine == true) {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(_startingPosition.x, _startingPosition.y), 2.0f * Time.deltaTime);
            if(transform.position == _startingPosition) {
                _isReelLine = false;
                gameObject.SetActive(false);
            }
        }
    }

    public void DropLine() {
        _isReelLine = false;
        _isDropLine = true;
    }

    public void ReelLine() {
        _isReelLine = true;
        _isDropLine = false;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == "Fish") {
            if(_hasHooked == false) {
                collider.gameObject.GetComponent<Fish>().Hooked();
                collider.gameObject.transform.position = transform.position;
                _hasHooked = true;
            }
        }
    }

}