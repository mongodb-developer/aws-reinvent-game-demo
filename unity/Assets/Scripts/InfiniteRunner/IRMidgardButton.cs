using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IRMidgardButton : MonoBehaviour {

    void OnMouseDown() {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MidgardScene");
    }

}