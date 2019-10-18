using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour{
    public float speed = 2f;
    public bool debugCharacter = false;
    
    protected Rigidbody2D myRigidBody;
    protected Animator myAnimator;

   public virtual void Start() {
        //Debug.Log("Character");
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        //Set initial orientation
        myAnimator.SetFloat("moveX", 0.0f);
        myAnimator.SetFloat("moveY", -1.0f);
    }


    public abstract void OnGetKicked(int attack);
}
