using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBolt : LinearBullet {

    public override void onCollision(Vector2 collisionPoint) {

        Vector2 hitDirection = collisionPoint - (Vector2)transform.position;
        myAnimator.SetFloat("moveX", hitDirection.x);
        myAnimator.SetFloat("moveY", hitDirection.y);

        myAnimator.SetTrigger("explode");
        transform.Find("trail").gameObject.SetActive(false);
        Destroy(GetComponent<Collider2D>());
        activeSpeed = 0;
        collided = true;
        Destroy(gameObject,2);
    }

    public override void setShotHeight(int height) {

        base.setShotHeight(height);
        TrailRenderer r = transform.Find("trail").gameObject.GetComponent<TrailRenderer>();
        r.sortingOrder = height;

        //pos.z -= 0.1f;
       // r.transform.position = pos;
        //Debug.Log("Trail:" + r.transform.position +"Order in Layer " + r.sortingOrder + "Layer name" + r.sortingLayerName + "|" + "Bullet" + renderer.transform.position + "Order in Layer " + renderer.sortingOrder + "Layer name" + renderer.sortingLayerName);
    }

    public override void shot(Vector2 direction) {


        //Debug.Log("Bullet start:" + transform.position + "Bullet direction:" + direction);


        
        gameObject.SetActive(true);
        transform.Find("trail").gameObject.SetActive(true);
        myAnimator.SetFloat("moveX", direction.x);
        myAnimator.SetFloat("moveY", direction.y);


        base.shot(direction);

        //this.direction = direction;
        //this.activeSpeed = speed;
        //Collider2D collider = this.GetComponent<Collider2D>();
        //collider.enabled = true;

        //Destroy(gameObject, lifetime);

    }
}
