using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {

    public Sprite fishSprite;
    public int fishWeight = 1;
    public float movementSpeed = 1.25f;
    public float movementTimeout = 10.0f;

    private bool _isMoving = false;
    private Vector2 _fishPosition;
    private float _timeUntilCanMove = 0.0f;
    private bool _isHooked = false;

    void OnEnable() {
        transform.position = new Vector3(Random.Range(-6.0f, 6.0f), Random.Range(-3.75f, 1.25f), 0.0f);
        fishWeight = Random.Range(1, 5);
    }

    void Start() {
        _fishPosition = new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(-3.75f, 1.25f));
        _isMoving = true;
    }

    void Update() {
        if(_isHooked == false) {
            _timeUntilCanMove -= Time.deltaTime;
            if(_isMoving == false && _timeUntilCanMove <= 0) {
                _fishPosition = new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(-3.75f, 1.25f));
                _isMoving = true;
            }
            if(_isMoving == true) {
                transform.position = Vector2.MoveTowards(transform.position, _fishPosition, movementSpeed * Time.deltaTime);
                if(transform.position == new Vector3(_fishPosition.x, _fishPosition.y, 0.0f)) {
                    _isMoving = false;
                    _timeUntilCanMove = movementTimeout;
                }
            }
        } else {
            if(GameObject.FindWithTag("Hook") != null) {
                transform.position = GameObject.FindWithTag("Hook").transform.position;
            }
        }
    }

    public void Hooked() {
        _isHooked = true;
    }

}
