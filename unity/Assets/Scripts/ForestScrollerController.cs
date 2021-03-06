using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ForestScrollerController : MonoBehaviour {

    private AudioSource _audioSource;
    private int _score;

    public GameObject mainMenuModal;
    public GameObject gameOverModal;
    public GameObject gameSuccessModal;
    public Text highScoreText;
    public Text scoreText;
    public Text timeRemainingText;
    public float playTime = 60.0f;
    public Text instructionsText;

    void Awake() {
        Time.timeScale = 1.0f;
        if(Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer) {
            instructionsText.gameObject.SetActive(true);
        }
    }

    void Start() {
        LevelManager.Instance.HideLoading();
        _score = 0;
        highScoreText.text = "HIGH SCORE: " + RealmController.Instance.GetCurrentPlayer().Games.ForestScroller.HighScore;
        _audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if(!LevelManager.Instance.IsLoading()) {
            scoreText.text = "SCORE: " + _score.ToString();
            timeRemainingText.text = "TIME REMAINING: " + ((int)playTime).ToString();
            playTime -= Time.deltaTime;
            if(playTime <= 0) {
                ShowGameOverModal();
            }
            if(Keyboard.current.escapeKey.wasReleasedThisFrame) {
                ToggleMainMenu();
            }
        }
    }

    public void IncreaseScore(int value) {
        _score += value;
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

    public void ShowGameOverModal() {
        gameOverModal.SetActive(true);
        Time.timeScale = 0.0f;
        RealmController.Instance.IncreaseForestScrollerPlayCount();
    }

    public void ShowGameSuccessModal() {
        gameSuccessModal.SetActive(true);
        Time.timeScale = 0.0f;
        RealmController.Instance.IncreaseForestScrollerPlayCount();
        RealmController.Instance.IncreaseForestScrollerScore(_score + ((int) playTime));
    }

}
