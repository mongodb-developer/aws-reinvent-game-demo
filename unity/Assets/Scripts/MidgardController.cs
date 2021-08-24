using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MidgardController : MonoBehaviour {

    public Text playerNameText;

    void Start() {
        playerNameText.text = "PLAYER: " + RealmController.Instance.GetCurrentPlayer().Email;
    }

    void Update() {
        
    }
}
