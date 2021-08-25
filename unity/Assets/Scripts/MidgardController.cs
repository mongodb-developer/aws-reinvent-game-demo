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
        playerNameText.text = "PLAYER: " + RealmController.Instance.GetCurrentPlayer().Email;
    }

    void Update() {
        totalScoreText.text = "TOTAL SCORE: " + RealmController.Instance.GetCurrentPlayer().TotalScore.ToString();
        if(GetKeyStrokeHistory().Equals("UpArrow,UpArrow,DownArrow,DownArrow,LeftArrow,RightArrow,LeftArrow,RightArrow,B,A")) {
            RealmController.Instance.SetTotalScore(1337);
            ClearKeyStrokeHistory();
        }
    }

    private void ToggleMainMenu() {
        mainMenuModal.SetActive(!mainMenuModal.activeInHierarchy);
        Time.timeScale = mainMenuModal.activeInHierarchy ? 0.0f : 1.0f;
    }

    void OnGUI() {
        Event e = Event.current;
        if(e.isKey && e.type == EventType.KeyUp) {
            if(e.keyCode == KeyCode.Escape) {
                ToggleMainMenu();
            } else {
                AddKeyStrokeToHistory(e.keyCode.ToString());
            }
        }
    }

    private void AddKeyStrokeToHistory(string keyStroke) {
        _keyStrokeHistory.Add(keyStroke);
        if(_keyStrokeHistory.Count > 10) {
            _keyStrokeHistory.RemoveAt(0);
        }
    }

    private string GetKeyStrokeHistory() {
        return String.Join(",", _keyStrokeHistory.ToArray());
    }

    private void ClearKeyStrokeHistory() {
        _keyStrokeHistory.Clear();
    }

}