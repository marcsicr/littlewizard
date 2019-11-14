using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBtn : Activator
{
    Animator myAnimator;

    private int objectsAbove = 0;
     protected override void Start() {

        base.Start();

        myAnimator = GetComponent<Animator>();
        myAnimator.SetBool("activate", false);

    }
    private void OnTriggerEnter2D(Collider2D other) {

        objectsAbove++;
        state = true;
        myAnimator.SetBool("activate", true);
        mechanism.notifyStatusChange(this);
    }

    private void OnTriggerExit2D(Collider2D other) {

        objectsAbove--;

        if(objectsAbove <= 0) {
            state = false;
            myAnimator.SetBool("activate", false);
            mechanism.notifyStatusChange(this);
        }
        
        
      
    }
}
