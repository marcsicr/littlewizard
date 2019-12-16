using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopesStun : StateMachineBehaviour {

    Cyclopes cyclope;
  

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        cyclope = animator.GetComponent<Cyclopes>();

        
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

       
                     
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
