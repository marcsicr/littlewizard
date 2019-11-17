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
    private bool collided = false;
    private void Awake() {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    

    // Update is called once per frame
    public virtual void FixedUpdate(){
        myRigidBody.velocity = direction * activeSpeed;
    }

    public abstract void shot(Vector2 direction);


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
   /* void OnTriggerEnter2D(Collider2D other) {
    
        if(target == BulletTarget.Player && other.tag == "Player") {
               
            Player p = other.GetComponent<Player>();
            onCollision();
            p.OnGetKicked(damage);
            
        } else if(target == BulletTarget.Enemy && other.tag == "Enemy") {

            Enemy e = other.GetComponent<Enemy>();
            onCollision();
           
            e.OnGetKicked(damage);
        } else {


            RaycastHit2D ray = Physics2D.Raycast(transform.position, direction);
            Vector2 hitDirection = (Vector2)transform.position - ray.point;

                myAnimator.SetFloat("moveX", direction.x);
                myAnimator.SetFloat("moveY", direction.y);
           
            

            //Debug.Log(ray.normal);

            //Debug.Log(otherPosition);

            onCollision();
            
        } 
    }*/

    /*Handle Bullet collision effects*/
    public abstract void onCollision(Vector2 collisionPoint);

    protected void startMoving(Vector2 direction) {

        this.direction = direction;
        this.activeSpeed = speed;
        Collider2D collider = this.GetComponent<Collider2D>();
        collider.enabled = true;

    }

    public Vector2 getDirection() {
        return direction;
    }
    public void setDirection(Vector2 direction) {

        this.direction = direction;
        
        //direction = Quaternion.AngleAxis(degrees, Vector3.forward) * direction;
    }
}
