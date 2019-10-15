using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : PlayerState {

    private float maxDist = 1f;
    float forceAxis = 10f;
    Vector3 clickPoint;
    Vector3 startPoint;

    
    public bool forceAplied;
    bool stop;
    bool done;
    public AttackState(Player player,Vector3 clickPoint) : base(player) { 
        this.clickPoint = clickPoint;
        this.clickPoint.z = 0;

        forceAplied = false;
        done = false;
        startPoint = new Vector3(player.transform.position.x,player.transform.position.y,0);
    }

    public override PlayerState handleInput() {

        if (Input.GetMouseButtonDown(0)) {
            playerAnimator.SetBool("walking", false); //Disable walking animation before going to attack
            return new AttackState(player, Camera.main.ScreenToWorldPoint(Input.mousePosition));

        }
        if (!attackIsDone()) {
            return this;
        } else {
            return player.idleState;
        }
    }

    public override void act() {

        
        if (!forceAplied) {

            
            player.StartCoroutine(StaffKickCo());

        } else {
            //Debug.Log("Velocity: " + playerRB.velocity.magnitude.ToString());
            //Check if we must stop
            if(Vector3.Distance(player.transform.position,startPoint) >= maxDist || playerRB.velocity.magnitude < 4f) {

               // Debug.Log("Velocity reset");
                playerRB.velocity = Vector2.zero;
                playerRB.angularVelocity = 0;
                stop = true;
            }
  
        }
    }

    IEnumerator StaffKickCo() {

        Vector2 direction = clickPoint - startPoint;
        direction.Normalize();
        playerAnimator.SetFloat("moveX", direction.x);
        playerAnimator.SetFloat("moveY", direction.y);
        playerAnimator.SetBool("attacking", true);
        player.decreaseSP();

        Vector2 force = new Vector2(forceAxis, forceAxis);
        playerRB.AddForce(force * direction, ForceMode2D.Impulse);
        forceAplied = true;
       
        yield return null;

      
        done = true;
        playerAnimator.SetBool("attacking", false);
        
        
    }
    public void setTarget() {

    }

    public bool attackIsDone() {

        return stop && done;
    }
    


}
