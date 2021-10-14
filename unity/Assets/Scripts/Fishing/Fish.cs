using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {

    public Sprite[] fishSprite;
    public int fishWeight = 1;
    public float movementSpeed = 1.25f;
    public float movementTimeout = 10.0f;

    private bool _isMoving = false;
    private Vector2 _fishPosition;
    private float _timeUntilCanMove = 0.0f;
    private bool _isHooked = false;
    private SpriteRenderer _spriteRenderer;

    void OnEnable() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = fishSprite[Random.Range(0, fishSprite.Length)];
        transform.position = new Vector3(Random.Range(-9.25f, 9.25f), Random.Range(-5.25f, 1.00f), 0.0f);
        fishWeight = Random.Range(1, 5);
    }

    void Start() {
        _fishPosition = new Vector2(Random.Range(-9.25f, 9.25f), Random.Range(-5.25f, 1.00f));
        if(_fishPosition.x < transform.position.x) {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        } else {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        _isMoving = true;
    }

    void Update() {
        if(_isHooked == false) {
            _timeUntilCanMove -= Time.deltaTime;
            if(_isMoving == false && _timeUntilCanMove <= 0) {
                _fishPosition = new Vector2(Random.Range(-9.25f, 9.25f), Random.Range(-5.25f, 1.00f));
                if(_fishPosition.x < transform.position.x) {
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                } else {
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }
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
