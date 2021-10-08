using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegistrationController : MonoBehaviour {

    public Button CancelButton;
    public Button RegistrationButton;
    public InputField NameInput;
    public InputField EmailInput;
    public InputField PasswordInput;

    void Awake() {
        Time.timeScale = 1.0f;
    }

    void Start() {
        NameInput.text = "Team Atlas";
        EmailInput.text = "reinvent@example.com";
        PasswordInput.text = "password1234";
        CancelButton.onClick.AddListener(Login);
        RegistrationButton.onClick.AddListener(Register);
    }
    
    public void Login() {
        SceneManager.LoadScene("LoginScene");
    }

    async public void Register() {
        if(await RealmController.Instance.Register(NameInput.text, EmailInput.text, PasswordInput.text) != "") {
            SceneManager.LoadScene("MidgardScene");
        }
    }

}