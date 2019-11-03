using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsBtn : MonoBehaviour
{
    GameObject settingsMenu;
    public void Awake() {

        settingsMenu = transform.parent.parent.Find("SettingsPanel").gameObject;
        if (settingsMenu == null) {
            Debug.Log("Missint settings Menu");
        }
    }

    public void Update() {
        
        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (!settingsMenu.activeInHierarchy)
                openSettingsMenu();
            else
                closeSettingsMenu();

        }
    }

    public void openSettingsMenu() {

            settingsMenu.SetActive(true);
            Time.timeScale = 0;
    }

    public void closeSettingsMenu() {
        Time.timeScale = 1;
        settingsMenu.SetActive(false); 
    }
}
