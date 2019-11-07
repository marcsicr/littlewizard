using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBolt : Bullet {
    public override void onCollision() {

        Destroy(gameObject);
    }

    public override void shot(Vector2 direction) {
        
        
        gameObject.SetActive(true);
        myAnimator.SetFloat("moveX", direction.x);
        myAnimator.SetFloat("moveY", direction.y);
        startMoving(direction);
        Destroy(gameObject, lifetime);

    }
}
