using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Activator
{
    private Animator myAnimator;
   
    private bool stager = false;
    protected override void Start() {

        base.Start();
        myAnimator = GetComponent<Animator>();
       
    }


    private void OnCollisionEnter2D(Collision2D other) {

        if (other.gameObject.CompareTag("Bullet")) {

            StartCoroutine(toggleStateCo());
        }
    }



    public IEnumerator toggleStateCo() {
        stager = true;
        state = !state;
        myAnimator.SetBool("activate", state);
       
        mechanism.notifyStatusChange(this);
        yield return new WaitForSeconds(0.2f);

        stager = false;

    }

    private void toggleState() {

        state = !state;

        myAnimator.SetBool("activate", state);

        mechanism.notifyStatusChange(this);
    }
}
