using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersionNumber : MonoBehaviour {

    private Text _versionNumberText;

    void Start() {
        _versionNumberText = GetComponent<Text>();
        _versionNumberText.text = "VERSION: " + Application.version;
    }

}
