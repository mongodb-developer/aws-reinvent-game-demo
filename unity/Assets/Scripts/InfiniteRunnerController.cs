using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteRunnerController : MonoBehaviour
{

    public float obstacleTimer = 2.0f;
    public GameObject gameOverModal;
    public Text scoreText;

    private float _timeUntilObstacle = 1.0f;
    private int _score;

    void Start() {
        _score = 0;
    }

    void Update() {
        _timeUntilObstacle -= Time.deltaTime;
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

}
