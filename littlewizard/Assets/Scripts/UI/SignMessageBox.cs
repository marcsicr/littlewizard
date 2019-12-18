using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignMessageBox : MonoBehaviour
{
    private TextMeshProUGUI text;
    bool showing = false;
    void Awake(){
      
        text = transform.Find("Text").GetComponent<TextMeshProUGUI>();
    }

    public void show(string message) {
        showing = true;
        text.text = message;
    }

    public void hide() {
        if (showing) {
            showing = false;
            Destroy(gameObject);
        }
       
    }
}
