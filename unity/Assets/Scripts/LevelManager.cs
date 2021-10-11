using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance;

    [SerializeField] private GameObject _loaderCanvas;
    [SerializeField] private Image _progressBar;
    private float _targetProgress;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void OnEnable() {
        _progressBar.fillAmount = 0;
        _targetProgress = 0;
    }

    public void ShowLoading() {
        _loaderCanvas.SetActive(true);
    }

    public void HideLoading() {
        _loaderCanvas.SetActive(false);
    }

    public void SetProgress(float progress) {
        _targetProgress = progress;
    }

    public async void LoadScene(string sceneName) {
        _progressBar.fillAmount = 0;
        _targetProgress = 0;
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;
        _loaderCanvas.SetActive(true);
        do {
            _targetProgress = scene.progress;
        } while (scene.progress < 0.9f);
        _targetProgress = 1.0f;
        await Task.Delay(1000);
        scene.allowSceneActivation = true;
        // _loaderCanvas.SetActive(false);
    }

    public void LoadSceneWithoutModal(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    void Update() {
        if(_loaderCanvas.activeInHierarchy) {
            _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _targetProgress, 3 * Time.deltaTime);
        }
    }

}
