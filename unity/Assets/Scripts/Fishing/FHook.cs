using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FHook : MonoBehaviour {

    private bool _isDropLine = false;
    private bool _isReelLine = false;
    private Vector3 _startingPosition;

    void OnEnable() {
        _startingPosition = transform.position;
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

}