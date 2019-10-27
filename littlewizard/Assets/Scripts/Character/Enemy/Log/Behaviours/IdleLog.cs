using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleLog : StateMachineBehaviour {
    Log log;
    float sleepTimeout = 3f;
    float timeToAttack;
    float timeToSleep;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        timeToSleep = Time.time + sleepTimeout;
        timeToAttack = Time.time; // Attack immediatly first, then use timeout;
        log = animator.gameObject.GetComponent<Log>();

        if (log == null)
            Debug.Log("Log component not found");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       
       
        if (log.isPlayerInChaseRadius() && !log.isPlayerInAttackRadius()) {
            animator.SetBool("chase", true);
        } else if(log.isPlayerInAttackRadius()) {


            Vector2 direction = log.getTargetDirection();
            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);
            log.rootAttack();
            timeToSleep = Time.time + sleepTimeout;
        }

        if (Time.time >= timeToSleep) {
            animator.SetBool("wakeUp", false);
        }
    }




    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Debug.Log("Leaving Attack State");

        log.resetSpeed();
    }


}
