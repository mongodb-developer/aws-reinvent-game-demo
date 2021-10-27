using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ValuePropositionSprite : MonoBehaviour {

    public UnityEvent onTriggerEnter2D;

    void OnTriggerEnter2D(Collider2D collider) {
        onTriggerEnter2D.Invoke();
    }

}