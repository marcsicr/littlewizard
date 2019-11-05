using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomChase : StateMachineBehaviour {

    Enemy enemy;
    //float nextAttack;
    //float delay = 1.5f;
    public float minPlayerDist = 3f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        enemy = animator.gameObject.GetComponent<Enemy>();
        if (enemy == null)
            Debug.Log("Enemy component not found");

        //nextAttack = Time.time + delay;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (!enemy.isPlayerInChaseRadius()) {
            animator.SetBool("chase", false);
            return;
        }

  
        animator.SetFloat("moveX", enemy.getTargetDirection().x);
        animator.SetFloat("moveY", enemy.getTargetDirection().y);

        if (enemy.isPlayerInAttackRadius() && enemy.isAttackReady()) {
            //Attack
            animator.SetTrigger("attack");
            return;

        } else if(enemy.distanceFromPlayer() >= minPlayerDist) {

            Vector3 enemyPos = animator.transform.position;
            Vector3 targetPos = enemy.getTarget().position;

            Vector3 step = Vector3.MoveTowards(enemyPos, targetPos, enemy.speed * Time.deltaTime);
            enemy.move(step);

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
