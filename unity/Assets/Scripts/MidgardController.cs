using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MidgardController : MonoBehaviour {

    public GameObject mainMenuModal;
    public Text playerNameText;
    public Text totalScoreText;

    private List<string> _keyStrokeHistory;

    void Awake() {
        _keyStrokeHistory = new List<string>();
    }

    void Start() {
        playerNameText.text = RealmController.Instance != null ? "PLAYER: " + RealmController.Instance.GetCurrentPlayer().Email : "PLAYER: ";
    }

    void Update() {
        totalScoreText.text = RealmController.Instance != null ? "TOTAL SCORE: " + RealmController.Instance.GetCurrentPlayer().TotalScore.ToString() : "TOTAL SCORE: ";
        KeyCode keyPressed = DetectKeyPressed();
        if(keyPressed == KeyCode.Escape) {
            ToggleMainMenu();
        } else {
            AddKeyStrokeToHistory(keyPressed.ToString());
        }
        if(GetKeyStrokeHistory().Equals("UpArrow,UpArrow,DownArrow,DownArrow,LeftArrow,RightArrow,LeftArrow,RightArrow,B,A")) {
            RealmController.Instance.SetTotalScore(1337);
            ClearKeyStrokeHistory();
        }
    }

    private void ToggleMainMenu() {
        mainMenuModal.SetActive(!mainMenuModal.activeInHierarchy);
        Time.timeScale = mainMenuModal.activeInHierarchy ? 0.0f : 1.0f;
    }

    private KeyCode DetectKeyPressed() {
        foreach(KeyCode key in Enum.GetValues(typeof(KeyCode))) {
            if(Input.GetKeyDown(key)) {
                return key;
            }
        }
        return KeyCode.None;
    }

    private void AddKeyStrokeToHistory(string keyStroke) {
        if(!keyStroke.Equals("None")) {
            _keyStrokeHistory.Add(keyStroke);
            if(_keyStrokeHistory.Count > 10) {
                _keyStrokeHistory.RemoveAt(0);
            }
        }
    }

    private string GetKeyStrokeHistory() {
        return String.Join(",", _keyStrokeHistory.ToArray());
    }

    private void ClearKeyStrokeHistory() {
        _keyStrokeHistory.Clear();
    }

}