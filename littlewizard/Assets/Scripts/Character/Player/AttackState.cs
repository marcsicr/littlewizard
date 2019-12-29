using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : PlayerState {

    private static float maxDist = 0.8f;

    
    Vector2 direction;
    
    public bool started_co;


    bool done = false;
    public AttackState(Player player,Vector3 direction) : base(player) {

        direction.Normalize();
        this.direction = direction;

        int randomIndex = Random.Range(0, player.staffKickClips.Length);
        SoundManager.Instance.playVoice(player.staffKickClips[randomIndex]);
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

     
        direction.x = Mathf.Round(direction.x);
        direction.y = Mathf.Round(direction.y);
       

        Vector3 destination = playerRB.position + direction.normalized * maxDist;
       Vector3 step = Vector3.Lerp(playerRB.transform.position, destination, 0.8f);
       yield return new WaitForFixedUpdate();
       playerRB.MovePosition(step);   
      

       step = Vector3.Lerp(playerRB.transform.position, destination, 0.6f);
       yield return new WaitForFixedUpdate();
       playerRB.MovePosition(destination);
       done = true;
    }
  
}
