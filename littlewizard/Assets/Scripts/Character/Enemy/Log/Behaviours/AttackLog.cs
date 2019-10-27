using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLog : StateMachineBehaviour {
    Log log;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

       
        log = animator.gameObject.GetComponent<Log>();

        if (log == null)
            Debug.Log("Log component not found");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (!log.isPlayerInAttackRadius()) {
            animator.SetBool("attack", false);
            return;
        }

        //Instantiate root attack; 
    }




    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Debug.Log("Leaving Attack State");

        log.resetSpeed();
    }


}
