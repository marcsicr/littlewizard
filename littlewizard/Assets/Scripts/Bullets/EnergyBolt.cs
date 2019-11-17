using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBolt : Bullet {
    public override void onCollision(Vector2 collisionPoint) {

        Vector2 hitDirection = collisionPoint - (Vector2)transform.position;
        myAnimator.SetFloat("moveX", hitDirection.x);
        myAnimator.SetFloat("moveY", hitDirection.y);

        myAnimator.SetTrigger("explode");
        Destroy(transform.Find("trail").gameObject);
        Destroy(GetComponent<Collider2D>());
        activeSpeed = 0;
        //Destroy(gameObject);
    }

    public override void shot(Vector2 direction) {
        
        
        
        gameObject.SetActive(true);
        transform.Find("trail").gameObject.SetActive(true);
        myAnimator.SetFloat("moveX", direction.x);
        myAnimator.SetFloat("moveY", direction.y);
        startMoving(direction);
        Destroy(gameObject, lifetime);

    }
}
