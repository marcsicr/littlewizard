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

    public void setBackgroundPanel(GameObject panel,GameObject selectedBackgound) {
        hiddenPanel = panel;
        hiddenPanel.SetActive(false);
        previousSelected = selectedBackgound;
    }

    public void closePanel() {

        hiddenPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(previousSelected);

        Destroy(gameObject);
    }

}
