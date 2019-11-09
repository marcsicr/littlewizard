using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolGoblinA : StateMachineBehaviour {

    GoblinA archer;
    float timeOutWalk = 4f;
    float nextWalk;
    Vector3 targetPosition;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        nextWalk = Time.time + timeOutWalk;
        archer = animator.gameObject.GetComponent<GoblinA>();

    }

    //If player is in attackRadius attack && ! in danger zone
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        if (archer.isPlayerInAttackRadius() && archer.isAttackReady()) {
            //animator.SetTrigger("shot");
            archer.attackAtempt();
            return;
        }else if(archer.isPlayerInAttackRadius())
        {
            return;
        }

        if (archer.isPlayerInChaseRadius()) {
            animator.SetBool("chase", true);
        }

        if(Time.time > nextWalk || isArcherOnTarget()) {
            //Pick Random point & go there
            int r = Random.Range(0, archer.patrolPoints.Length-1);
            Vector2 point = archer.patrolPoints[r];

            //Convert local space position to world space
            targetPosition = archer.transform.parent.TransformPoint(new Vector3(point.x, point.y, archer.transform.position.z));
            nextWalk = Time.time + timeOutWalk;
            //Debug.Log("New Archer target " + targetPosition);
           
        }

        Vector3 step = Vector3.MoveTowards(archer.transform.position, targetPosition, archer.speed * Time.deltaTime);
        Vector3 faceDirection = Vector3.Normalize(step - archer.transform.position);
        animator.SetFloat("moveX", faceDirection.x);
        animator.SetFloat("moveY", faceDirection.y);

        archer.move(step);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        animator.SetBool("patrol", false);
    }

    public bool isArcherOnTarget() {

        return Vector3.Distance(archer.transform.position, targetPosition) < 0.01f;
    }
}
