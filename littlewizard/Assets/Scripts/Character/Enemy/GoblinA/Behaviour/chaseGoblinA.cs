using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chaseGoblinA : StateMachineBehaviour {

    GoblinA archer;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        archer = animator.gameObject.GetComponent<GoblinA>();
    }

    //If player is in attackRadius attack && ! in danger zone
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (archer.isPlayerInAttackRadius() && archer.isAttackReady()) {
            animator.SetTrigger("shot");
            return;
        }

        if (!archer.isPlayerInChaseRadius()) {
            animator.SetBool("chase", false);
            animator.SetBool("patrol", false); //To return to idle
            return;
        }


        Vector3 enemyPos = animator.transform.position;
        Vector3 targetPos = archer.getTarget().position;

        Vector3 step = Vector3.MoveTowards(enemyPos, targetPos, archer.speed * Time.deltaTime);
        Vector3 faceDirection = Vector3.Normalize(step - enemyPos);
        animator.SetFloat("moveX", faceDirection.x);
        animator.SetFloat("moveY", faceDirection.y);

        archer.move(step);

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        animator.SetBool("chase", false);
    }

}
