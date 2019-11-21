using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pushable : MonoBehaviour {

    [SerializeField] private Rigidbody2D myRigidbody;


    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("Enemy")) {
            myRigidbody.velocity = Vector2.zero;
            myRigidbody.angularVelocity = 0;
        }
        
        if (collision.gameObject.CompareTag("Bullet")) {
            StartCoroutine(stopHitCo());
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.CompareTag("Enemy")){

            myRigidbody.isKinematic = true;
            //myRigidbody.bodyType = RigidbodyType2D.Static;
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {


        myRigidbody.velocity = Vector2.zero;
        myRigidbody.angularVelocity = 0;
    }

    private IEnumerator stopHitCo() { 

        yield return new WaitForSeconds(0.3f);
        myRigidbody.velocity = Vector2.zero;
        myRigidbody.angularVelocity = 0;
    }

    private void OnTriggerExit2D(Collider2D other) {

        if (other.CompareTag("Enemy")) {
            myRigidbody.isKinematic = false;
            //myRigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
