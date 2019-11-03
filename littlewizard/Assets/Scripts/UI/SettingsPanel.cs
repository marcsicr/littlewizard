using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanel : MonoBehaviour {

    public void resumeGame() {

        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void exitGame() {

        Application.Quit();
    }

} 
