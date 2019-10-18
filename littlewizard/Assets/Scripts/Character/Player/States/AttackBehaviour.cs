using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour {

    Player player;
    Rigidbody2D playerRB;
    Vector2 force;  
    float maxDistance = 1f;
    Vector3 startPosition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        player =  GameObject.FindObjectOfType<Player>();
        playerRB = player.GetComponent<Rigidbody2D>();

        startPosition = player.transform.position;
        force = new Vector2(10f, 10f);
       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        //Delegar a FixedUpdate aquesta operacio
        player.StartCoroutine(StaffKickCo(animator));
       
    }


    IEnumerator StaffKickCo(Animator animator) {

        float xDirection = animator.GetFloat("attackX");
        float yDirection = animator.GetFloat("attackY");
        Vector2 forceDirection = new Vector2(xDirection, yDirection);
        
        playerRB.AddForce(forceDirection.normalized * force, ForceMode2D.Impulse);

        while (Vector3.Distance(player.transform.position, startPosition) < maxDistance) {
            yield return null;
        } 
            
            
        playerRB.velocity = Vector2.zero;
        playerRB.angularVelocity = 0;
        animator.SetBool("attacking", false);
      
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Debug.Log("Leaving Attack State");

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
