using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    /*Player can go from Idle->Attack or Idle->Walk*/
    public IdleState(Player player) : base(player) { }


    public override PlayerState handleInput() {

        if (Input.GetMouseButtonDown(0)) {
            return new AttackState(player, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

       
        
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) {

            player.walkState.change.x = Input.GetAxisRaw("Horizontal");
            player.walkState.change.y = Input.GetAxisRaw("Vertical");
            return player.walkState;
        }

        return this;
    }
    public override void act() {
      
        //Nothing
    }

   
}
