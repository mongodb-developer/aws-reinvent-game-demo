using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class RegistrationController : MonoBehaviour {

    public Button CancelButton;
    public Button RegistrationButton;
    public InputField NameInput;
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
        NameInput.text = "";
        EmailInput.text = "";
        PasswordInput.text = "";
        if(Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer) {
            NameInput.Select();
        }
        CancelButton.onClick.AddListener(Login);
        RegistrationButton.onClick.AddListener(Register);
    }

    void Update() {
        if(Keyboard.current.escapeKey.wasReleasedThisFrame) {
            Application.Quit();
        } else if(Keyboard.current.tabKey.wasReleasedThisFrame) {
            _inputFieldIndex++;
            switch(_inputFieldIndex) {
                case 0:
                    NameInput.Select();
                    break;
                case 1:
                    EmailInput.Select();
                    break;
                case 2:
                    PasswordInput.Select();
                    break;
                default:
                    _inputFieldIndex = 0;
                    if(Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer) {
                        NameInput.Select();
                    }
                    break;
            }
        } else if(Keyboard.current.enterKey.wasReleasedThisFrame) {
            Register();
        }
    }
    
    public void Login() {
        LevelManager.Instance.LoadSceneWithoutModal("LoginScene");
    }

    async public void Register() {
        LevelManager.Instance.ShowLoading();
        LevelManager.Instance.SetProgress(0.3f);
        string registrationResponse = await RealmController.Instance.Register(NameInput.text, EmailInput.text, PasswordInput.text);
        if(registrationResponse == "") {
            LevelManager.Instance.LoadScene("StoryStartingScene");
        } else {
            ErrorText.gameObject.SetActive(true);
            ErrorText.text = "ERROR: " + registrationResponse;
            LevelManager.Instance.HideLoading();
        }
    }

}