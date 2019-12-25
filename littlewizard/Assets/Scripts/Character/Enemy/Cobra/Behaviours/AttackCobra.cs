using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCobra : StateMachineBehaviour {
    Enemy enemy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        enemy = animator.gameObject.GetComponent<Enemy>();//.GetComponent<Cobra>();
        if (enemy == null)
            Debug.Log("Enemy component not found");

        //enemy.resetSpeed();


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.SetBool("attack", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

       
    }

}
