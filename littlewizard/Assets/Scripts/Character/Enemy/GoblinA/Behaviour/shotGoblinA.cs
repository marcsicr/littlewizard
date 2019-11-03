using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotGoblinA : StateMachineBehaviour
{
    GoblinA archer;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

       /* archer = animator.gameObject.GetComponent<GoblinA>();
        Vector2 face = archer.getTargetDirection();
        animator.SetFloat("moveX", face.x);
        animator.SetFloat("moveY", face.y);*/

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

       // archer.attackAtempt();
        animator.SetBool("shot", false);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

    }

}
