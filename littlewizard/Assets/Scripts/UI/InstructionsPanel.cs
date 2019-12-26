using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InstructionsPanel : MonoBehaviour
{
    public GameObject message1;
    public GameObject message2;
    public GameObject message3;

    public GameObject firstBtn;
    public GameObject secondBtn;
    public GameObject thirdBtn;
   
    void Start() {

        SelectBtn(firstBtn);
    }

    public void showMessage2() {

        message1.SetActive(false);
        message2.SetActive(true);
        SelectBtn(secondBtn);
    }

    public void showMessage3() {
        message2.SetActive(false);
        message3.SetActive(true);
        SelectBtn(thirdBtn);
    }

    public void hideInstructions() {
        Destroy(gameObject);
    }

    void SelectBtn(GameObject btnGO) {

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(btnGO);
    }
}
