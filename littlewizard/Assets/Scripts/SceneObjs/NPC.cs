using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour{


    public DialogMessage[] messages;
   
    bool showing = false;
    private int spaceHits;
    bool inRange = false;

    public void showDialog() {

       showing = true;
       DialogManager.Instance.displayConversation(messages);
    }

   


    

    private void Update() {

        if (inRange && Input.GetKeyDown(KeyCode.Space ) && !showing) {
                showDialog();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Player")) {

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
            showing = false;
            
        }
    }
}
