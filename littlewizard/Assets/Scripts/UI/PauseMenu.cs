﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    GameObject panel;
    Button resumeBtn;
    
    public void Awake() {

      panel =  transform.Find("SettingsPanel").gameObject;
      resumeBtn = panel.transform.Find("ResumeBtn").GetComponent<Button>();
    }

    public void Start() {
        panel.SetActive(true);
        resumeBtn.Select();

    }

    public void close() {
       EventSystem.current.SetSelectedGameObject(null);
        Destroy(gameObject);
       
    }

    public void resume() {

        GameManager.Instance.resumeGame();
     
    }

    public void ExitGameBtnClick() {

        GameManager.Instance.QuitGame();
    }

    

} 
