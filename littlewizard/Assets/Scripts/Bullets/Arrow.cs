using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Bullet
{
    public override void FixedUpdate() {

        myRigidBody.velocity = direction * activeSpeed;
    }

    public override void shot(Vector2 direction) {
        this.gameObject.SetActive(true);

        startMoving(direction);
        myAnimator.SetFloat("moveX", direction.x);
        myAnimator.SetFloat("moveY", direction.y);
        Destroy(gameObject, lifetime);
    }

    public override void onCollision(Vector2 collisionPoint) {
        Destroy(gameObject);
    }
}
