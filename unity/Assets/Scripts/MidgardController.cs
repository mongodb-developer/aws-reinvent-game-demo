using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MidgardController : MonoBehaviour {

    public Text playerNameText;
    public Text totalScoreText;

    void Start() {
        playerNameText.text = "PLAYER: " + RealmController.Instance.GetCurrentPlayer().Email;
        totalScoreText.text = "TOTAL SCORE: " + RealmController.Instance.GetCurrentPlayer().TotalScore.ToString();
    }

    void Update() {
        
    }

}