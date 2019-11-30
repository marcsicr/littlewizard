using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBolt : LinearBullet {
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


        //Debug.Log("Bullet start:" + transform.position + "Bullet direction:" + direction);
        
        gameObject.SetActive(true);
        transform.Find("trail").gameObject.SetActive(true);
        myAnimator.SetFloat("moveX", direction.x);
        myAnimator.SetFloat("moveY", direction.y);

        this.direction = direction;
        this.activeSpeed = speed;
        Collider2D collider = this.GetComponent<Collider2D>();
        collider.enabled = true;

        Destroy(gameObject, lifetime);

    }
}
