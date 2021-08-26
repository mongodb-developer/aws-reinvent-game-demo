using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteRunnerController : MonoBehaviour
{

    public float obstacleTimer = 1.5f;
    public GameObject gameOverModal;
    public GameObject gameSuccessModal;
    public Text scoreText;

    private float _timeUntilObstacle = 1.0f;
    private int _score;
    private float _acceleration = 1.0f;

    void Start() {
        _score = 0;
    }

    void Update() {
        if((int) Time.timeSinceLevelLoad >= 100) {
            ShowGameSuccessModal();
        } else if((int) Time.timeSinceLevelLoad >= 50) {
            _acceleration = 2.50f;
        } else if((int) Time.timeSinceLevelLoad >= 40) {
            _acceleration = 2.25f;
        } else if((int) Time.timeSinceLevelLoad >= 30) {
            _acceleration = 1.75f;
        } else if((int) Time.timeSinceLevelLoad >= 20) {
            _acceleration = 1.50f;
        } else if((int) Time.timeSinceLevelLoad >= 10) {
            _acceleration = 1.25f;
        }
        _timeUntilObstacle -= Time.deltaTime * _acceleration;
        if(_timeUntilObstacle <= 0) {
            GameObject obstacle = IRObjectPool.SharedInstance.GetPooledObstacle();
            if(obstacle != null) {
                obstacle.SetActive(true);
            }
            _timeUntilObstacle = obstacleTimer;
        }
        _score = (int) Time.timeSinceLevelLoad;
        scoreText.text = "SCORE: " + _score.ToString();
    }

    public void ShowGameOverModal() {
        gameOverModal.SetActive(true);
        Time.timeScale = 0.0f;
        RealmController.Instance.IncreaseChangeStreamsPlayCount();
        RealmController.Instance.IncreaseChangeStreamsScore(_score);
    }

    public void ShowGameSuccessModal() {
        gameSuccessModal.SetActive(true);
        Time.timeScale = 0.0f;
        RealmController.Instance.IncreaseChangeStreamsPlayCount();
        RealmController.Instance.IncreaseChangeStreamsScore(_score);
    }

}
