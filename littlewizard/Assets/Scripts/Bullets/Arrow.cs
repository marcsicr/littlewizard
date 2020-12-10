using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : ParabolicBullet{

    public override void onCollision(Vector2 collisionPoint) {

        SoundManager.Instance.playEffect(bulletHitClip);
        Destroy(gameObject);
    }
}
