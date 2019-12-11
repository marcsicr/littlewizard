using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsBtn : MonoBehaviour{
   
    public void settingsBtnClick() {

        if (!GameManager.Instance.isGamePaused) {

            GameManager.Instance.pauseGame();
        } else {

            GameManager.Instance.resumeGame();
        }
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {

            settingsBtnClick();
        }
    }
}
