using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopesPatrol : StateMachineBehaviour {

    Cyclopes cyclope;
    Rigidbody2D myRigidBody;
    Vector3[] patrolPoints;
    float timeOut = 0.8f;
 
   
    Coroutine m_patrolCo;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        cyclope = animator.GetComponent<Cyclopes>();
        myRigidBody = cyclope.GetComponent<Rigidbody2D>();
        patrolPoints = cyclope.patrolPoints;

        m_patrolCo = cyclope.StartCoroutine(PatrolCo(animator));

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        timeOut -= Time.deltaTime;
      
        if(timeOut <= 0) {
            chooseNextState(animator);
            timeOut = 0.8f;
        }
       
                     
    }

    private IEnumerator PatrolCo(Animator myAnimator) {


        while (true) {
            yield return new WaitForFixedUpdate();

            if (Vector3.Distance(cyclope.transform.position, patrolPoints[cyclope.patrolIndex]) <= cyclope.speed) {

                //Update target
                if (cyclope.patrolIndex < patrolPoints.Length - 1) {
                    cyclope.patrolIndex ++;
                } else {
                    cyclope.patrolIndex = 0;
                }
            }

            Vector3 step = Vector3.MoveTowards(cyclope.transform.position, patrolPoints[cyclope.patrolIndex], cyclope.speed * Time.fixedDeltaTime);
            myRigidBody.MovePosition(step);

            Vector2 dir = (step - cyclope.transform.position).normalized;
            myAnimator.SetFloat("moveX", dir.x);
            myAnimator.SetFloat("moveY", dir.y);

        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        cyclope.StopCoroutine(m_patrolCo);
    }

    void chooseNextState(Animator animator) {



        if (cyclope.BossSP.getRunTimeValue() == cyclope.BossSP.initialValue && !cyclope.isLaserStateSheduled) {
            Debug.Log("Scheduling");
            cyclope.sheduleLaserAttack();
        }

        if (cyclope.isStunReady && cyclope.distanceFromPlayer() < cyclope.discoverDistance) {

            animator.SetTrigger("stunJump");
            return;
        }

        if (cyclope.distanceFromPlayer() < cyclope.discoverDistance) {
            animator.SetTrigger("throw");
            return;
        }
      
    }
}
