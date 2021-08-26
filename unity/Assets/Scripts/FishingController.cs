using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingController : MonoBehaviour {

    public Text scoreText;

    private int _score;

    void Start() {
        _score = 0;
        for(int i = 0; i < 5; i++) {
            GameObject fish = FObjectPool.SharedInstance.GetPooledFish();
            if(fish != null) {
                fish.SetActive(true);
            }
        }
    }

    void Update() {
        scoreText.text = "SCORE: " + _score.ToString();
    }

    public void IncreaseScore(int weight) {
        _score += weight;
    }

}