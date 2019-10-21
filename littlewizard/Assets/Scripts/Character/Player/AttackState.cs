using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : PlayerState {

    private float maxDist = 0.8f;
    float forceAxis = 10f;
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
            player.StartCoroutine(StaffKickCo());
            started_co = true;
        }

    }

    IEnumerator StaffKickCo() {

        Vector3 destination = playerRB.position + direction * maxDist;
        Vector3 step = Vector3.Lerp(playerRB.transform.position, destination, 0.7f);
        playerRB.MovePosition(step);
        yield return null;

        step = Vector3.Lerp(playerRB.transform.position, destination, 0.6f);
        playerRB.MovePosition(step);
        yield return null;

        playerRB.MovePosition(destination);
        done = true;
     }
  
}
