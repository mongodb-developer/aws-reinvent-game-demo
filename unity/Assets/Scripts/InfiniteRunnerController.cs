using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteRunnerController : MonoBehaviour
{

    public float obstacleTimer = 2.0f;
    public GameObject gameOverModal;

    private float _timeUntilObstacle = 1.0f;

    void Update() {
        _timeUntilObstacle -= Time.deltaTime;
        if(_timeUntilObstacle <= 0) {
            GameObject obstacle = IRObjectPool.SharedInstance.GetPooledObstacle();
            if(obstacle != null) {
                obstacle.SetActive(true);
            }
            _timeUntilObstacle = obstacleTimer;
        }
    }

    public void ShowGameOverModal() {
        gameOverModal.SetActive(true);
        Time.timeScale = 0.0f;
    }

}
