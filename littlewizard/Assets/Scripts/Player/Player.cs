using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 2f;
    private Vector2 change;
    private Rigidbody2D myRigidBody;
    private Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        
        //Set player initial orientation
        myAnimator.SetFloat("moveX", 0.0f);
        myAnimator.SetFloat("moveY", -1.0f);

    }

    // Update is called once per frame
    void Update()
    {
        change = Vector2.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

      

        if (change != Vector2.zero)
        {
            change.Normalize();
            myRigidBody.MovePosition((Vector2)transform.position + change * speed * Time.deltaTime);
            myAnimator.SetBool("walking", true);
            myAnimator.SetFloat("moveX", change.x);
            myAnimator.SetFloat("moveY", change.y);
        }
        else
        {
            myAnimator.SetBool("walking", false);
        }

        

    }
}