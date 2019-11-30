using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBullet : LinearBullet
{
    public override void shot(Vector2 direction) {

        base.shot(Vector2.zero);
        base.direction = direction;
    }

    public IEnumerator explodeCo() { 
        speed = 0;
        yield return null;
        Destroy(gameObject);
    }

    public override void onCollision(Vector2 collisionPoint) {
       StartCoroutine(explodeCo());
    }

}
