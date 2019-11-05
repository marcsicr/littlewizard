using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBullet : Bullet
{
    public override void shot(Vector2 direction) {

        //Set direction && Make bullet visible && set speed
        this.gameObject.SetActive(true);
        startMoving(direction);
        Destroy(gameObject, lifetime);

    }

    public IEnumerator explodeCo() { 
        speed = 0;
        yield return null;
        Destroy(gameObject);
    }

    public override void onCollision() {
       StartCoroutine(explodeCo());
    }

   

}
