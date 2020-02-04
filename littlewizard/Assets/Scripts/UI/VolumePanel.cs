using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VolumePanel : MonoBehaviour
{
    GameObject hiddenPanel;
    GameObject previousSelected;
    public GameObject slider;

  
    void Start() {

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(slider);
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            closePanel();
        }
    }

    public void setBackgroundPanel(GameObject panel,GameObject selectedBackgound) {
        hiddenPanel = panel;
        hiddenPanel.SetActive(false);
        previousSelected = selectedBackgound;
    }

    public void closePanel() {

        if (hiddenPanel != null) {

   
            hiddenPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(previousSelected);

        }

        Destroy(gameObject);
    }

}
