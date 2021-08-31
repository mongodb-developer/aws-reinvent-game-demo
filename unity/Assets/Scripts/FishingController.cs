using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingController : MonoBehaviour {

    public Text scoreText;
    public GameObject gameSuccessModal;
    public GameObject mainMenuModal;
    public Text timeRemainingText;
    public float playTime = 100.0f;

    private int _score;

    void Start() {
        _score = 0;
        for(int i = 0; i < FObjectPool.SharedInstance.fishPoolSize; i++) {
            GameObject fish = FObjectPool.SharedInstance.GetPooledFish();
            if(fish != null) {
                fish.SetActive(true);
            }
        }
    }

    void Update() {
        scoreText.text = "SCORE: " + _score.ToString();
        timeRemainingText.text = "TIME REMAINING: " + ((int) playTime).ToString();
        playTime -= Time.deltaTime;
        if((FObjectPool.SharedInstance.IsPoolFull() == true || playTime <= 0) && gameSuccessModal.activeInHierarchy == false) {
            ShowGameSuccessModal();
        }
        if(Input.GetKeyUp(KeyCode.Escape)) {
            ToggleMainMenu();
        }
    }

    public void IncreaseScore(int weight) {
        _score += weight;
    }

    private void ToggleMainMenu() {
        mainMenuModal.SetActive(!mainMenuModal.activeInHierarchy);
        Time.timeScale = mainMenuModal.activeInHierarchy ? 0.0f : 1.0f;
    }

    public void ShowGameSuccessModal() {
        gameSuccessModal.SetActive(true);
        Time.timeScale = 0.0f;
        RealmController.Instance.IncreaseFishingPlayCount();
        RealmController.Instance.IncreaseFishingScore(_score);
    }

}