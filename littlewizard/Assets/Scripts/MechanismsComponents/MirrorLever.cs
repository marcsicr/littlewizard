using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorLever : MonoBehaviour
{
    private Animator myAnimator;
    private RotableMirror mirror;
    bool changingState = false;
    bool state = false;
    
    void Awake() {
        myAnimator = GetComponent<Animator>();
        mirror = transform.GetComponentInParent<RotableMirror>();
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

        mirror.rotate();
        yield return new WaitForSeconds(0.2f);
        changingState = false;


    }

   
   
}
