using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestScrollerController : MonoBehaviour {

    private AudioSource _audioSource;

    public GameObject mainMenuModal;

    void Start() {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        if(Input.GetKeyUp(KeyCode.Escape)) {
            ToggleMainMenu();
        }
    }

    private void ToggleMainMenu() {
        mainMenuModal.SetActive(!mainMenuModal.activeInHierarchy);
        Time.timeScale = mainMenuModal.activeInHierarchy ? 0.0f : 1.0f;
        if(!mainMenuModal.activeInHierarchy) {
            _audioSource.UnPause();
        } else {
            _audioSource.Pause();
        }
    }

}
