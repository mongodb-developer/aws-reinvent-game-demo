using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LoginController : MonoBehaviour {

    public Button LoginButton;
    public Button RegistrationButton;
    public InputField EmailInput;
    public InputField PasswordInput;
    public Text ErrorText;

    void Awake() {
        Time.timeScale = 1.0f;
        ErrorText.gameObject.SetActive(false);
    }

    void Start() {
        LevelManager.Instance.HideLoading();
        EmailInput.text = "";
        PasswordInput.text = "";
        LoginButton.onClick.AddListener(Login);
        RegistrationButton.onClick.AddListener(Register);
    }

    void Update() {
        if(Keyboard.current.escapeKey.wasReleasedThisFrame) {
            Application.Quit();
        }
    }
    
    async public void Login() {
        LevelManager.Instance.ShowLoading();
        LevelManager.Instance.SetProgress(0.3f);
        string loginResponse = await RealmController.Instance.Login(EmailInput.text, PasswordInput.text);
        if(loginResponse == "") {
            // SceneManager.LoadScene("MidgardScene");
            LevelManager.Instance.LoadScene("MidgardScene");
        } else {
            ErrorText.gameObject.SetActive(true);
            ErrorText.text = "ERROR: " + loginResponse;
            LevelManager.Instance.HideLoading();
        }
    }

    public void Register() {
        // SceneManager.LoadScene("RegistrationScene");
        LevelManager.Instance.LoadSceneWithoutModal("RegistrationScene");
    }

}
