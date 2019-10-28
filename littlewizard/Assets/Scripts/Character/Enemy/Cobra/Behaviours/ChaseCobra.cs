using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCobra : StateMachineBehaviour {

    Cobra cobra;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        cobra = animator.gameObject.GetComponent<Cobra>();
        if (cobra == null)
            Debug.Log("Cobra component not found");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {


        if(cobra.distanceFromPlayer() <= cobra.minDistance) {
            animator.SetBool("chase", false);
            return;
        }

        if (!cobra.isPlayerInChaseRadius()) {
            animator.SetBool("chase", false);
            return;
        }

        Debug.Log("Distance from player " + cobra.distanceFromPlayer());
        // In chase Radius and distance from player > minDistance

        Vector3 enemyPos = animator.transform.position;
        Vector3 targetPos = cobra.getTarget().position;

        Vector3 step = Vector3.MoveTowards(enemyPos, targetPos, cobra.speed * Time.deltaTime);
        Vector3 faceDirection = Vector3.Normalize(step - enemyPos);
        animator.SetFloat("moveX", faceDirection.x);
        animator.SetFloat("moveY", faceDirection.y);

        cobra.move(step);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Debug.Log("Leaving Walk State");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
