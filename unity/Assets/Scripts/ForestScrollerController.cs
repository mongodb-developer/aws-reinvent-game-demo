using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForestScrollerController : MonoBehaviour {

    private AudioSource _audioSource;
    private int _score;

    public GameObject mainMenuModal;
    public GameObject gameOverModal;
    public GameObject gameSuccessModal;
    public Text scoreText;

    void Start() {
        _score = 0;
        _audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        scoreText.text = "SCORE: " + _score.ToString();
        if(Input.GetKeyUp(KeyCode.Escape)) {
            ToggleMainMenu();
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
        RealmController.Instance.IncreaseForestScrollerScore(_score);
    }

}
