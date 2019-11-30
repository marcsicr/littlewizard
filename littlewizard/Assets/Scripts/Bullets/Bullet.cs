﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum BulletTarget { Player, Enemy };
public abstract class Bullet : MonoBehaviour
{
    public int damage;
    public int lifetime;
    
    public int speed;
    protected int activeSpeed;

    public BulletTarget target;
    protected Rigidbody2D myRigidBody;
    protected Animator myAnimator;
    
    
    private bool collided = false;

    public abstract void shot(Vector2 worldPoint);
    
    /*Handle Bullet collision effects*/
    public abstract void onCollision(Vector2 collisionPoint);

    private void Awake() {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }



    private void OnCollisionEnter2D(Collision2D other ) {

        if (collided)
            return;

        ContactPoint2D contactPoint = other.GetContact(0);
       // Vector2 hitDirection = point.point - (Vector2)transform.position;

        if (target == BulletTarget.Player && other.gameObject.tag == "Player") {
           
            Player p = other.gameObject.GetComponent<Player>();
            onCollision(contactPoint.point);
            p.OnGetKicked(damage);

        } else if (target == BulletTarget.Enemy && other.gameObject.tag == "Enemy") {

           
            Enemy e = other.gameObject.GetComponent<Enemy>();
            onCollision(contactPoint.point);
            e.OnGetKicked(damage);
            
           
        } else {

            if (other.gameObject.tag == "Pot") {
                Pot pot = other.gameObject.GetComponent<Pot>();
                pot.destroy();
            }

                onCollision(contactPoint.point);

        }

        collided = true;


    }

   
}
