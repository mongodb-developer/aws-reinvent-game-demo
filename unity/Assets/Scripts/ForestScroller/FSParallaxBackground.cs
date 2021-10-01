using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSParallaxBackground : MonoBehaviour {

    public float parallaxEffectMultiplier;

    private Transform _camera;
    private Vector3 _lastCameraPosition;
    private float _textureUnitSizeX;

    void Start() {
        _camera = Camera.main.transform;
        _lastCameraPosition = _camera.position;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.size = new Vector2(spriteRenderer.size.x * 3, spriteRenderer.size.y);
        Sprite sprite = spriteRenderer.sprite;
        Texture2D texture = sprite.texture;
        _textureUnitSizeX = (texture.width / sprite.pixelsPerUnit) * transform.localScale.x;
    }

    void FixedUpdate() {
        Vector3 deltaMovement = _camera.position - _lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier, deltaMovement.y);
        _lastCameraPosition = _camera.position;

        if(Mathf.Abs(_camera.position.x - transform.position.x) >= _textureUnitSizeX) {
            float offsetPositionX = (_camera.position.x - transform.position.x) % _textureUnitSizeX;
            transform.position = new Vector3(_camera.position.x + offsetPositionX, transform.position.y);
        }
    }

}
