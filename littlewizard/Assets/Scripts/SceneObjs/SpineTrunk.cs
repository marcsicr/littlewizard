using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineTrunk : MonoBehaviour
{
    public enum MovingDirection { Horizontal,Vertical};

    public bool flip;
    public MovingDirection direction;
    private Rigidbody2D myRigidbody;
    
    public float speed;
    public float distance;
    private float moveOrientation = 1;

    private Vector2 startPos;
    private Vector2 vDirection;
    void Start(){
;
        if(direction == MovingDirection.Horizontal) {

            vDirection = Vector2.right;
        } else {

            vDirection = Vector2.up;
        }

        if (flip) {
            moveOrientation = -1;
        }

        myRigidbody = GetComponent<Rigidbody2D>();
        startPos = myRigidbody.position;
    }

    // Update is called once per frame
    void Update(){

        Vector2 newPos = startPos + Mathf.Sin(Time.time * speed) * distance * vDirection * moveOrientation;
        myRigidbody.MovePosition(newPos);
    }

    private void OnCollisionStay2D(Collision2D other) {

            if (other.gameObject.CompareTag(Player.TAG)) {

                Player p = other.gameObject.GetComponent<Player>();
                p.OnGetKicked(1);
            }
        
    }
}
