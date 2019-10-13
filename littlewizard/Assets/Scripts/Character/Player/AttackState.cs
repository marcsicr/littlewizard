using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : PlayerState {

    private float maxDist = 1.5f;
    float forceAxis = 7f;
    Vector3 clickPoint;
    Vector3 startPoint;

    
    public bool forceAplied;
    bool done;
    public AttackState(Player player,Vector3 clickPoint) : base(player) { 
        this.clickPoint = clickPoint;
        this.clickPoint.z = 0;

        forceAplied = false;
        startPoint = new Vector3(player.transform.position.x,player.transform.position.y,0);
    }

    public override PlayerState handleInput() {

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
            Debug.Log("Velocity: " + playerRB.velocity.magnitude.ToString());
            //Check if we must stop
            if(Vector3.Distance(player.transform.position,startPoint) >= maxDist || playerRB.velocity.magnitude < 4f) {

               // Debug.Log("Velocity reset");
                playerRB.velocity = Vector2.zero;
                playerRB.angularVelocity = 0;
                done = true;
            }else
            {
                if(playerRB.velocity == Vector2.zero) {
                    //Debug.Log("Player velocity is zero");
                   //Fix stuck
                   
                
                }
            }
  
        }
      


        //playerRB.AddForce(Vector2, ForceMode2D.Impulse);    
        //Moure el jugador en la direccio del click.
        //player.transform.position = Vector3.MoveTowards(player.transform.position, clickPoint, player.speed * Time.deltaTime);


        //playerRB.MovePosition((Vector2)player.transform.position + (Vector2)step * player.speed * Time.deltaTime);
    }

    IEnumerator StaffKickCo() {

        Vector2 direction = clickPoint - startPoint;
        direction.Normalize();
        playerAnimator.SetFloat("moveX", direction.x);
        playerAnimator.SetFloat("moveY", direction.y);
        player.decreaseSP();

        Vector2 force = new Vector2(forceAxis, forceAxis);
        playerRB.AddForce(force * direction, ForceMode2D.Impulse);
        forceAplied = true;
        playerAnimator.SetBool("attacking", true);
        yield return null;

        playerAnimator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.2f);
        
    }
    public void setTarget() {

    }

    public bool attackIsDone() {

        return done;
    }
    


}
