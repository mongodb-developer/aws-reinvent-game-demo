using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MidgardController : MonoBehaviour {

    public Text playerNameText;

    void Start() {
        // Debug.Log(RealmController.Instance.GetAuthId());
        // Debug.Log(RealmController.Instance.GetCurrentPlayer().Email);
        playerNameText.text = "PLAYER: " + RealmController.Instance.GetCurrentPlayer().Email;
    }

    void Update() {
        
    }
}
