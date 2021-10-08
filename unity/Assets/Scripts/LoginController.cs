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

    void Awake() {
        Time.timeScale = 1.0f;
    }

    void Start() {
        EmailInput.text = "reinvent@example.com";
        PasswordInput.text = "password1234";
        LoginButton.onClick.AddListener(Login);
        RegistrationButton.onClick.AddListener(Register);
    }

    void Update() {
        if(Keyboard.current.escapeKey.wasReleasedThisFrame) {
            Application.Quit();
        }
    }
    
    async public void Login() {
        if(await RealmController.Instance.Login(EmailInput.text, PasswordInput.text) != "") {
            SceneManager.LoadScene("MidgardScene");
        }
    }

    public void Register() {
        SceneManager.LoadScene("RegistrationScene");
    }

}
