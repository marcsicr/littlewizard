using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : PlayerState {

    public Vector2 change;
 

    public WalkState(Player player) : base(player) {
        change = Vector2.zero;
    
    }
   

    public override PlayerState handleInput() {

        //Check first if we should change to another state
        if (Input.GetMouseButtonDown(0)) {
            playerAnimator.SetBool("walking", false); //Disable walking animation before going to attack
            return new AttackState(player, Camera.main.ScreenToWorldPoint(Input.mousePosition));
         
        }
            


        //We mantain the the player in this frame on walking state

        change = Vector2.zero; //Resset change between calls
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (change != Vector2.zero) {
            return this;
        } else {

            playerAnimator.SetBool("walking", false); //Disable walking animation before going to idle
            return player.idleState;
        }
          
    }

    public override void act() {

        if (change != Vector2.zero) {
            playerAnimator.SetFloat("moveX", change.x);
            playerAnimator.SetFloat("moveY", change.y);
            playerAnimator.SetBool("walking", true);
            change.Normalize();
            playerRB.MovePosition((Vector2)player.transform.position + change * player.speed * Time.deltaTime);
        }
    }


}