using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogBox : MonoBehaviour
{
    private TextMeshProUGUI text;
    void Awake()
    {
        text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }



    public void show(string message) {

        
        gameObject.SetActive(true);
        text.text = message;
    }

    public void hide() {
        gameObject.SetActive(false);
    }
}
