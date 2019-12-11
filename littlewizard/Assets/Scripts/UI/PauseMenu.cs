using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    GameObject panel;
    Button resumeBtn;
    public void Awake() {

      panel =  transform.Find("SettingsPanel").gameObject;
      panel.SetActive(false);
      resumeBtn = panel.transform.Find("ResumeBtn").GetComponent<Button>();

    }

    public void show() {
        panel.SetActive(true);
        resumeBtn.Select();

    }

    public void hide() {
       panel.SetActive(false);
       EventSystem.current.SetSelectedGameObject(null);
    }

    public void resume() {

        GameManager.Instance.resumeGame();
    }

    public void ExitGameBtnClick() {

        GameManager.Instance.QuitGame();
    }

    

} 
