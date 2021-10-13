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

    private int _inputFieldIndex = 0;

    void Awake() {
        Time.timeScale = 1.0f;
        ErrorText.gameObject.SetActive(false);
    }

    void Start() {
        LevelManager.Instance.HideLoading();
        EmailInput.text = "";
        PasswordInput.text = "";
        if(Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer) {
            EmailInput.Select();
        }
        LoginButton.onClick.AddListener(Login);
        RegistrationButton.onClick.AddListener(Register);
    }

    void Update() {
        if(Keyboard.current.escapeKey.wasReleasedThisFrame) {
            Application.Quit();
        } else if(Keyboard.current.tabKey.wasReleasedThisFrame) {
            _inputFieldIndex++;
            switch(_inputFieldIndex) {
                case 0:
                    EmailInput.Select();
                    break;
                case 1:
                    PasswordInput.Select();
                    break;
                default:
                    _inputFieldIndex = 0;
                    if(Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer) {
                        EmailInput.Select();
                    }
                    break;
            }
        } else if(Keyboard.current.enterKey.wasReleasedThisFrame) {
            Login();
        }
    }
    
    async public void Login() {
        LevelManager.Instance.ShowLoading();
        LevelManager.Instance.SetProgress(0.3f);
        string loginResponse = await RealmController.Instance.Login(EmailInput.text, PasswordInput.text);
        if(loginResponse == "") {
            LevelManager.Instance.LoadScene("MidgardScene");
        } else {
            ErrorText.gameObject.SetActive(true);
            ErrorText.text = "ERROR: " + loginResponse;
            LevelManager.Instance.HideLoading();
        }
    }

    public void Register() {
        LevelManager.Instance.LoadSceneWithoutModal("RegistrationScene");
    }

}
