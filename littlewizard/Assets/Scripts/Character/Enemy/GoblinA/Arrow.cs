using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;
    Vector2 direction;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    private float activeSpeed;
    
    void Awake() {

        myRigidBody = gameObject.GetComponent<Rigidbody2D>();
        myAnimator = gameObject.GetComponent<Animator>();
        activeSpeed = 0;
    }

    public void shot(Vector2 direction) {
        this.gameObject.SetActive(true);
        this.direction = direction;
        activeSpeed = speed;
        myAnimator.SetFloat("moveX", direction.x);
        myAnimator.SetFloat("moveY", direction.y);
    }

    private void FixedUpdate() {

            myRigidBody.velocity = direction * activeSpeed;
     }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.tag == "Player") {

            Player p = other.gameObject.GetComponent<Player>();
            p.OnGetKicked(25);
            Destroy(gameObject);
        }


    }
}
