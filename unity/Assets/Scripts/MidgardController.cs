using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;

public class MidgardController : MonoBehaviour {

    public GameObject mainMenuModal;
    public Text playerNameText;
    public Text totalScoreText;

    private List<string> _keyStrokeHistory;
    private AudioSource _audioSource;

    void Awake() {
        Time.timeScale = 1.0f;
        _keyStrokeHistory = new List<string>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Start() {
        playerNameText.text = RealmController.Instance != null ? "PLAYER: " + RealmController.Instance.GetCurrentPlayer().Email : "PLAYER: ";
    }

    void Update() {
        totalScoreText.text = RealmController.Instance != null ? "TOTAL SCORE: " + RealmController.Instance.GetCurrentPlayer().TotalScore.ToString() : "TOTAL SCORE: ";
        Key keyPressed = DetectKeyPressed();
        if(keyPressed == Key.Escape) {
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
        if(!mainMenuModal.activeInHierarchy) {
            _audioSource.UnPause();
        } else {
            _audioSource.Pause();
        }
    }

    private Key DetectKeyPressed() {
        foreach(Key key in Enum.GetValues(typeof(Key))) {
            if(key != Key.IMESelected) {
                if(key != Key.None && Keyboard.current[key].wasPressedThisFrame) {
                    return key;
                }
            }
        }
        return Key.None;
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