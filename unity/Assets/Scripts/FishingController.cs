using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class FishingController : MonoBehaviour {

    public Text highScoreText;
    public Text scoreText;
    public GameObject gameSuccessModal;
    public GameObject mainMenuModal;
    public Text timeRemainingText;
    public float playTime = 60.0f;
    public Text instructionsText;

    private int _score;
    private AudioSource _audioSource;

    void Awake() {
        Time.timeScale = 1.0f;
        _audioSource = GetComponent<AudioSource>();
        if(Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer) {
            instructionsText.gameObject.SetActive(true);
        }
    }

    void Start() {
        LevelManager.Instance.HideLoading();
        highScoreText.text = "HIGH SCORE: " + RealmController.Instance.GetCurrentPlayer().Games.Fishing.HighScore;
        _score = 0;
        for(int i = 0; i < FObjectPool.SharedInstance.fishPoolSize; i++) {
            GameObject fish = FObjectPool.SharedInstance.GetPooledFish();
            if(fish != null) {
                fish.SetActive(true);
            }
        }
    }

    void Update() {
        if(!LevelManager.Instance.IsLoading()) {
            scoreText.text = "SCORE: " + _score.ToString();
            timeRemainingText.text = "TIME REMAINING: " + ((int) playTime).ToString();
            playTime -= Time.deltaTime;
            if((FObjectPool.SharedInstance.IsPoolFull() == true || playTime <= 0) && gameSuccessModal.activeInHierarchy == false) {
                ShowGameSuccessModal();
            }
            if(Keyboard.current.escapeKey.wasReleasedThisFrame) {
                ToggleMainMenu();
            }
        }
    }

    public void IncreaseScore(int weight) {
        _score += weight;
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

    public void ShowGameSuccessModal() {
        gameSuccessModal.SetActive(true);
        Time.timeScale = 0.0f;
        RealmController.Instance.IncreaseFishingPlayCount();
        RealmController.Instance.IncreaseFishingScore(_score);
    }

}