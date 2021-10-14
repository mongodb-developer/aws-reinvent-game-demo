using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InfiniteRunnerController : MonoBehaviour
{

    public float obstacleTimer = 1.5f;
    public GameObject gameOverModal;
    public GameObject gameSuccessModal;
    public GameObject mainMenuModal;
    public Text highScoreText;
    public Text scoreText;
    public Text instructionsText;

    private float _timeUntilObstacle = 1.0f;
    private int _score;
    private float _acceleration = 1.0f;
    private Component[] _audioSources;

    void Awake() {
        Time.timeScale = 1.0f;
        _audioSources = GetComponents(typeof(AudioSource));
        if(Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer) {
            instructionsText.gameObject.SetActive(true);
        }
    }

    void Start() {
        LevelManager.Instance.HideLoading();
        _score = 0;
        highScoreText.text = "HIGH SCORE: " + RealmController.Instance.GetCurrentPlayer().Games.ChangeStreams.HighScore;
    }

    void Update() {
        if(!LevelManager.Instance.IsLoading()) {
            if(Keyboard.current.escapeKey.wasReleasedThisFrame) {
                ToggleMainMenu();
            }
            if((int) Time.timeSinceLevelLoad >= 100 && gameSuccessModal.activeInHierarchy == false) {
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
    }

    private void ToggleMainMenu() {
        mainMenuModal.SetActive(!mainMenuModal.activeInHierarchy);
        Time.timeScale = mainMenuModal.activeInHierarchy ? 0.0f : 1.0f;
        if(!mainMenuModal.activeInHierarchy) {
            foreach(AudioSource _audioSource in _audioSources) {
                _audioSource.UnPause();
            }
        } else {
            foreach(AudioSource _audioSource in _audioSources) {
                _audioSource.Pause();
            }
        }
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
