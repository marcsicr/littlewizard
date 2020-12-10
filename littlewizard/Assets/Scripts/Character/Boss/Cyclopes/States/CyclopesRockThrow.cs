using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopesRockThrow : StateMachineBehaviour {

    Cyclopes cyclope;
  

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        cyclope = animator.GetComponent<Cyclopes>();

       Vector2 direction = cyclope.getDirectionToPlayer();
        animator.SetFloat("throwX", direction.x);
        animator.SetFloat("throwY", direction.x);

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        Vector2 direction = cyclope.getDirectionToPlayer();
        animator.SetFloat("throwX", direction.x);
        animator.SetFloat("throwY", direction.x);


    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       
    }
    
}
