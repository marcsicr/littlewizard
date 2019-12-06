﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LinearBullet : Bullet{
    
    protected Vector2 direction;
    protected int shotHeight;

    public virtual void FixedUpdate() {
        myRigidBody.velocity = direction * activeSpeed;
    }


    public Vector2 getDirection() {
        return direction;
    }
    public void setDirection(Vector2 direction) {

        this.direction = direction;

    }

    private void Update() {

        if (!collided) {

            if (LevelManager.Instance.getTileLevel(transform.position) > shotHeight) {
                Debug.Log("Destroyed by level");
                onCollision(transform.position);
            }
        }
    }

    public virtual void setShotHeight(int height) {

        shotHeight = height;
        GetComponent<SpriteRenderer>().sortingOrder = height;
    }

    public override void shot(Vector2 point) {

        direction = (point - (Vector2)transform.position).normalized;
        gameObject.SetActive(true);

        this.activeSpeed = speed;
        Collider2D collider = this.GetComponent<Collider2D>();
        collider.enabled = true;

       // Destroy(gameObject, lifetime);

    }

    protected override void OnCollisionEnter2D(Collision2D other) {

        ContactPoint2D contactPoint = other.GetContact(0);

        int heightOfCollision = LevelManager.Instance.getTileLevel(contactPoint.point);

        if (heightOfCollision >= shotHeight) {

            base.OnCollisionEnter2D(other);

        } else {

            Debug.Log("Height Of collision" + heightOfCollision.ToString());
            Physics2D.IgnoreCollision(other.collider, gameObject.GetComponent<Collider2D>());
           
        }
        

    }
}
