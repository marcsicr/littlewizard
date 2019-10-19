﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseBehaviour : StateMachineBehaviour {

    Enemy enemy;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        enemy = animator.gameObject.GetComponent<Enemy>();
        if (enemy == null)
            Debug.Log("Enemy component not found");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (enemy.isPlayerInAttackRadius()) {
            //Attack
            animator.SetBool("attack",true);

        } else if(enemy.isPlayerInChaseRadius()) {

            Vector3 enemyPos = animator.transform.position;
            Vector3 targetPos = enemy.getTarget().position;

            Vector3 step = Vector3.MoveTowards(enemyPos, targetPos, enemy.speed * Time.deltaTime);
            Vector3 faceDirection = Vector3.Normalize(step - enemyPos);
            animator.SetFloat("moveX", faceDirection.x);
            animator.SetFloat("moveY", faceDirection.y);

            enemy.move(step);
        } else {

            animator.SetBool("chase", false);
        }
        
       

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
