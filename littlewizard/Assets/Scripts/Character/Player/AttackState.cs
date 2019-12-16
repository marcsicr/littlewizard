using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : PlayerState {

    private static float maxDist = 0.8f;

    Vector3 clickPoint;
    Vector2 direction;
    Vector3 startPoint;
    public bool started_co;


    bool done;
    public AttackState(Player player,Vector3 clickPoint) : base(player) { 
        this.clickPoint = clickPoint;
        this.clickPoint.z = 0;

       

        done = false;
        startPoint = new Vector3(playerRB.position.x, playerRB.position.y,0);

        direction = clickPoint - startPoint;
        direction.Normalize();

        playerAnimator.SetTrigger("attack");

    }

    public override PlayerState handleInput() {

     
        if (done) {
            return player.idleState;
        } else {
            return this;
        }
    }

    public override void act() {

        if (!started_co) {

            playerAnimator.SetFloat("attackX", direction.x);
            playerAnimator.SetFloat("attackY", direction.y);

            playerAnimator.SetFloat("moveX", direction.normalized.x);
            playerAnimator.SetFloat("moveY", direction.normalized.y);

            player.StartCoroutine(StaffKickCo());
            

            player.decreaseStamina(1);
           
            started_co = true;
        }

        //Debug.Log("Attack state");
    }

    IEnumerator StaffKickCo() {


       Vector3 destination = playerRB.position + direction * maxDist;
       Vector3 step = Vector3.Lerp(playerRB.transform.position, destination, 0.8f);
       yield return new WaitForFixedUpdate();
       playerRB.MovePosition(step);   
      

       step = Vector3.Lerp(playerRB.transform.position, destination, 0.6f);
       yield return new WaitForFixedUpdate();
       playerRB.MovePosition(destination);
       done = true;
    }
  
}
