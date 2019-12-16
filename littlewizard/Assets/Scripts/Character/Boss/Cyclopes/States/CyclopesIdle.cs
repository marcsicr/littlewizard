using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopesIdle : StateMachineBehaviour {

    Cyclopes cyclope;
    float timeOut = 0.8f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        cyclope = animator.GetComponent<Cyclopes>();
        
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        timeOut -= Time.deltaTime;
        Vector2 direction = cyclope.getDirectionToPlayer();
        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);
        animator.SetFloat("throwX", direction.x);
        animator.SetFloat("throwY", direction.y);

        if(timeOut <= 0) {
            chooseNextState(animator);
            timeOut = 0.8f;
        }
       
                     
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       
    }


    void chooseNextState(Animator animator) {

       if(cyclope.getDistanceToPlayer() < cyclope.discoverDistance) {
            animator.SetTrigger("throw");
       } else {

            animator.SetBool("patrol", true);
       }
    }
    
}
