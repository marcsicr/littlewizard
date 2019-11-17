using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pushable : MonoBehaviour {

    [SerializeField] private Rigidbody2D myRigidbody;
   

    private void OnCollisionExit2D(Collision2D collision) {

        myRigidbody.velocity = Vector2.zero;
        myRigidbody.angularVelocity = 0;
    }
 
}
