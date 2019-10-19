using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character{

    //States that can be reused
    public IdleState idleState; 
    public WalkState walkState; 

    public IntVar playerHP;
    public IntVar playerSP;
    public IntVar stamina;

    private Vector2 movement;
    private Vector2 clickPoint;
    private PlayerState currentState;
    
    public override void Start() {
        base.Start();

        idleState = new IdleState(this);
        walkState = new WalkState(this);
        currentState = idleState;
        
    }

   public Animator getAnimator() {

        return this.myAnimator;
    }

    public void Update() {
       currentState = currentState.handleInput();
    }


    public void FixedUpdate() {

        currentState.act();
    }


    public void decreaseSP() {
        this.playerSP.runtimeValue -= 5;
    }


    private void handleInput() {

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (movement != Vector2.zero) {
            myAnimator.SetFloat("moveX", movement.x);
            myAnimator.SetFloat("moveY", movement.y);
        }

        myAnimator.SetFloat("magnitude", movement.sqrMagnitude);

        if (Input.GetMouseButton(0)) {
           clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           Vector2 direction =  clickPoint - myRigidBody.position;
           myAnimator.SetFloat("attackX", direction.x);
           myAnimator.SetFloat("attackY", direction.y);
           myAnimator.SetBool("attacking", true);
        }
      

    }

    public override void OnGetKicked(int attack) {
        Debug.Log("Player got kicked" + playerHP.runtimeValue.ToString());
        
        if(playerHP.runtimeValue > 0) {

            if (playerHP.runtimeValue > attack) {

                playerHP.runtimeValue -= attack;
            } else {
                playerHP.runtimeValue = 0;
            }
        }
       
    }

}