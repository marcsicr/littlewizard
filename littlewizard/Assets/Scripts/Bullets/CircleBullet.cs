using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBullet : LinearBullet
{
    public override void shot(Vector2 direction) {

        base.shot(Vector2.zero);
        base.direction = direction;
    }

    public IEnumerator explodeCo() {
        activeSpeed = 0;
        myAnimator.SetTrigger("explode");
        Debug.Log("Mutiple calls?");
       
        yield return new WaitForSeconds(0.5f);
        
        Destroy(gameObject);
    }

    public override void onCollision(Vector2 collisionPoint) {

        StartCoroutine(explodeCo());
        
    }

}
