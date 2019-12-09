using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Activator
{
    private Animator myAnimator;
   bool changingState = false;
    protected override void Start() {

        base.Start();
        myAnimator = GetComponent<Animator>();
       
    }


    private void OnCollisionEnter2D(Collision2D other) {

        if (other.gameObject.CompareTag("Bullet")) {
            StartCoroutine(toggleStateCo());
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {

       

        if (other.gameObject.CompareTag("Staff") && !changingState) {
            changingState = true;
            Debug.Log("Staff");
            StartCoroutine(toggleStateCo());
        }

    }



    public IEnumerator toggleStateCo() {
        
        state = !state;
        myAnimator.SetBool("activate", state);
       
        mechanism.notifyStatusChange(this);
        yield return new WaitForSeconds(0.2f);
        changingState = false;
    

    }

    public override void Reset() {
        state = false;
        myAnimator.SetBool("activate", false);
    }

    /*public void toggletState() {

    }*/
}
