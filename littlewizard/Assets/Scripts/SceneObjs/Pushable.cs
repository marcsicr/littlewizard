using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pushable : MonoBehaviour {

    [SerializeField] private Rigidbody2D myRigidbody;


    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("Bullet")) {
            StartCoroutine(stopHitCo());
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
 
}
