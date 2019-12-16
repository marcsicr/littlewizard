using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopesGoingToShot : StateMachineBehaviour {

    Cyclopes cyclope;
    Coroutine m_gointToShot;
   
    

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

       
       cyclope = animator.GetComponent<Cyclopes>();
       m_gointToShot= cyclope.StartCoroutine(WalkToSpawnPoint(animator));
       
       
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
                   
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        Vector3 dir = cyclope.getDirectionToPlayer();
        animator.SetFloat("moveX", dir.x);
        animator.SetFloat("moveY", dir.y);
       
    }

    
    

    private IEnumerator WalkToSpawnPoint(Animator myAnimator) {

      
        Vector3 spawnLocation = cyclope.getSpawnLocation();
        Rigidbody2D myRigidBody = cyclope.GetComponent<Rigidbody2D>();
        float walkStep = cyclope.speed;

        
        while (Vector3.Distance(cyclope.transform.position, spawnLocation) > 0.01f) {
            Vector3 step = Vector3.MoveTowards(cyclope.transform.position, spawnLocation, walkStep * Time.fixedDeltaTime);
            Vector3 dir = (step - cyclope.transform.position).normalized;
            myAnimator.SetFloat("moveX", dir.x);
            myAnimator.SetFloat("moveY", dir.y);
            myRigidBody.MovePosition(step);
            yield return new WaitForFixedUpdate();
        }

        myAnimator.SetBool("shooting", true);

    }

}
