using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoutButton : MonoBehaviour {

    void OnMouseDown() {
        RealmController.Instance.Logout();
        SceneManager.LoadScene("LoginScene");
    }

}