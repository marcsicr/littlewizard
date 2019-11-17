﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public DialogBox textBox;
    public string message;
    private bool inRange = false;
    private bool showing = false;

    private void Update() {
        
        if(inRange && Input.GetKeyDown(KeyCode.Space)) {

            if (!showing) {
                showing = true;
                textBox.show(message);
                Debug.Log("Show message");

            } else{
                textBox.hide();
                showing = false;
            }
            
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.CompareTag("Player")) {

            Player p = other.gameObject.GetComponent<Player>();
            p.showAlertBubble(true);
            inRange = true;
           
        }
    }

    private void OnTriggerExit2D(Collider2D other) {

        if (other.CompareTag("Player")) {

            Player p = other.gameObject.GetComponent<Player>();
            p.showAlertBubble(false);
            inRange = false;
            textBox.hide();
        }
    }


}
