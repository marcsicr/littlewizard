using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleGoblinA : StateMachineBehaviour {

    GoblinA archer;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        archer = animator.gameObject.GetComponent<GoblinA>();

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (archer.isPlayerInAttackRadius() && archer.isAttackReady()) {
            animator.SetTrigger("shot");
            return;
        } else if (archer.isPlayerInAttackRadius()) {
            return;
        }

        if (archer.isPlayerInChaseRadius()) {
            animator.SetBool("chase", true);
            return;
        }

        //If is not in attack or chase radius then patrol
        animator.SetBool("patrol", true);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       
    }

}
