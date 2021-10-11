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
    public Text ErrorText;

    void Awake() {
        Time.timeScale = 1.0f;
        ErrorText.gameObject.SetActive(false);
    }

    void Start() {
        LevelManager.Instance.HideLoading();
        NameInput.text = "";
        EmailInput.text = "";
        PasswordInput.text = "";
        CancelButton.onClick.AddListener(Login);
        RegistrationButton.onClick.AddListener(Register);
    }
    
    public void Login() {
        // SceneManager.LoadScene("LoginScene");
        LevelManager.Instance.LoadSceneWithoutModal("LoginScene");
    }

    async public void Register() {
        LevelManager.Instance.ShowLoading();
        LevelManager.Instance.SetProgress(0.3f);
        string registrationResponse = await RealmController.Instance.Register(NameInput.text, EmailInput.text, PasswordInput.text);
        if(registrationResponse == "") {
            // SceneManager.LoadScene("MidgardScene");
            LevelManager.Instance.LoadScene("MidgardScene");
        } else {
            ErrorText.gameObject.SetActive(true);
            ErrorText.text = "ERROR: " + registrationResponse;
            LevelManager.Instance.HideLoading();
        }
    }

}