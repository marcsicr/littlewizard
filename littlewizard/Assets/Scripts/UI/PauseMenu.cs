using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    GameObject panel;

    public void Awake() {

      panel =  transform.Find("SettingsPanel").gameObject;
      panel.SetActive(false);
    }

    public void show() {
        panel.SetActive(true);
    }

    public void hide() {
       panel.SetActive(false);
    }

    public void resume() {

        GameManager.Instance.resumeGame();
    }

    public void ExitGameBtnClick() {

        GameManager.Instance.QuitGame();
    }

    

} 
