using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryMenuButton : MonoBehaviour {

    private Button _button;

    void Awake() {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(btnClick);
    }
    
    void btnClick() {
        Time.timeScale = 1.0f;
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        LevelManager.Instance.LoadScene(SceneManager.GetActiveScene().name);
    }

}
