using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnReset : MonoBehaviour
{
    Mechanism mechanism;
    Animator myAnimator;
    Pushable[] pushables;
    private int objectsAbove = 0;

    private void Awake() {

        pushables = gameObject.GetComponentsInChildren<Pushable>();
        myAnimator = GetComponent<Animator>();
        mechanism = transform.parent.parent.GetComponent<Mechanism>();

        Debug.Log("pushables size:" + pushables.Length);
    }

    private void OnTriggerEnter2D(Collider2D other) {

        objectsAbove++;
        if (other.CompareTag(Player.TAG)) {
            myAnimator.SetBool("pushed", true);
            mechanism.Reset();
            
            foreach(Pushable p in pushables) {

                p.restartPosition();
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) {

        objectsAbove--;
        if (objectsAbove <= 0) {
            myAnimator.SetBool("pushed", false);
        }



    }
}
