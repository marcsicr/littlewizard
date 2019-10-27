////using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Log : Enemy
//{
//    private enum State { sleep,walk,attack,death};

//    private State logState;
//    // Start is called before the first frame update

  
//    public override void  Start(){
//        base.Start();
//        logState = State.sleep;

//    }

//    public void Update(){
//        if(isPlayerInChaseRadius() && !isPlayerInAttackRadius()){
            
//            if (logState == State.walk){
              
//                Vector3 step = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
//                Vector3 faceDirection = Vector3.Normalize(step - transform.position);
//                myAnimator.SetFloat("moveX", faceDirection.x);
//                myAnimator.SetFloat("moveY", faceDirection.y);

//                myRigidBody.MovePosition((Vector2)step);
//            }else{
//                myAnimator.SetBool("wakeUp", true); //Posaru en una corutina? - Perque camini quan estigui despert.
//                logState = State.walk;
//            }

//        }else if (isPlayerInAttackRadius()){
//            //Run attack animation
//            logState = State.attack;
//        } else {
//            myAnimator.SetFloat("moveX", 0);
//            myAnimator.SetFloat("moveY", -1);
//            myAnimator.SetBool("wakeUp", false);
//            logState = State.sleep;
//        }
//    }

//    public override void OnGetKicked(int attack) {

//        Destroy(this.gameObject);
//    }
//    public void FixedUpdate() {
        
//        if (isPlayerInChaseRadius()){
//            //WakeUp
//            //FollowPlayer

//            if (isPlayerInAttackRadius()){
//                //Atack
//            }
//        }
        
//    }

//}
