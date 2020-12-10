using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : ParabolicBullet
{
    private static int SHOT_ROTATION = 5;
    public override void onCollision(Vector2 collisionPoint) {

        Destroy(gameObject);
        
    }


    public override void shotDirectionUpdate(float t, Vector2 start, Vector2 parabolicMiddle, Vector2 end) {

        Vector2 direction = end - start;
        float rotation;
        rotation = direction.x >= 0 ? -SHOT_ROTATION : SHOT_ROTATION;
        transform.Rotate(Vector3.forward * rotation);
    }

}
