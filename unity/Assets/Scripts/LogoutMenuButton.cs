using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogoutMenuButton : MonoBehaviour {
    
    private Button _button;

    void Awake() {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(btnClick);
    }

    void btnClick() {
        RealmController.Instance.Logout();
        LevelManager.Instance.LoadScene("LoginScene");
    }

}