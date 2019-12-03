using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseLog : StateMachineBehaviour {
    Log log;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

       
        log = animator.gameObject.GetComponent<Log>();

        if (log == null)
            Debug.Log("Log component not found");

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {



        if (log.distanceFromPlayer() <= log.getMinDistance()) {
            animator.SetBool("chase", false);
            return;
        }
            

        if (!log.isPlayerInChaseRadius()) {
            animator.SetBool("chase", false);
            return;
        }

        if (log.isPlayerInAttackRadius()) {
            log.attackAtempt(); // rootAttack();

        }


        Vector3 logPos = animator.transform.position;
        Vector3 targetPos = log.getPlayerTransform().position;

        Vector3 step = Vector3.MoveTowards(logPos, targetPos, log.speed * Time.deltaTime);
        Vector3 faceDirection = Vector3.Normalize(step - logPos);
        animator.SetFloat("moveX", faceDirection.x);
        animator.SetFloat("moveY", faceDirection.y);

        log.move(step);
       

    }




    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Debug.Log("Leaving Attack State");

        log.resetSpeed();
    }


}

