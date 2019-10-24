using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour {

    Enemy enemy;
   

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        enemy = animator.gameObject.GetComponent<Enemy>();
        
        if (enemy == null)
            Debug.Log("Enemy component not found");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        //Update Attack Orientation

        if (enemy.isPlayerInAttackRadius()) {

            Vector2 direction = enemy.getTargetDirection();
            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);
            


        } else {

            animator.SetBool("attack", false);
        }
       
    }


  

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Debug.Log("Leaving Attack State");
        
        enemy.resetSpeed();
    }


 

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
