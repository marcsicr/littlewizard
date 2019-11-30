using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LinearBullet : Bullet{
    
    protected Vector2 direction;

  
    public virtual void FixedUpdate() {
        myRigidBody.velocity = direction * activeSpeed;
    }


    public Vector2 getDirection() {
        return direction;
    }
    public void setDirection(Vector2 direction) {

        this.direction = direction;

    }

    public override void shot(Vector2 point) {

        direction = (point - (Vector2)transform.position).normalized;
        gameObject.SetActive(true);

        this.activeSpeed = speed;
        Collider2D collider = this.GetComponent<Collider2D>();
        collider.enabled = true;

        Destroy(gameObject, lifetime);

    }
}
