using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogBox : MonoBehaviour
{
    private TextMeshProUGUI text;
    public GameObject panel;
    void Awake()
    {
        panel.SetActive(false);
        text = transform.Find("Panel/Text").GetComponent<TextMeshProUGUI>();
    }



    public void show(string message) {

        
        panel.SetActive(true);
        text.text = message;
    }

    public void hide() {
        panel.SetActive(false);
    }
}
