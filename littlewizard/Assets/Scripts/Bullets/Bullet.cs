using System.Collections;
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
    protected Vector2 direction;

    private void Awake() {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public virtual void FixedUpdate(){
        myRigidBody.velocity = direction * activeSpeed;
    }

    public abstract void shot(Vector2 direction);

    void OnTriggerEnter2D(Collider2D other) {
    
        if(target == BulletTarget.Player && other.tag == "Player") {
               
            Player p = other.GetComponent<Player>();
            onCollision();
            p.OnGetKicked(damage);
            
        } else if(target == BulletTarget.Enemy && other.tag == "Enemy") {

            Enemy e = other.GetComponent<Enemy>();
            onCollision();
            e.OnGetKicked(damage);
        }   
    }

    /*Handle Bullet collision effects*/
    public abstract void onCollision();

    protected void startMoving(Vector2 direction) {

        this.direction = direction;
        this.activeSpeed = speed;
    }
}
