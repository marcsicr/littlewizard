using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character{

    //States that can be reused
    public IdleState idleState; 
    public WalkState walkState; 

    public IntVar playerHP;
    public IntVar playerSP;
    public IntVar stamina;

    private Vector2 movement;
    private Vector2 clickPoint;
    private PlayerState currentState;
    
    public override void Start() {
        base.Start();

        idleState = new IdleState(this);
        walkState = new WalkState(this);
        currentState = idleState;
        
    }

   public Animator getAnimator() {

        return this.myAnimator;
    }

    public void Update() {
       currentState = currentState.handleInput();
    }


    public void FixedUpdate() {

        currentState.act();

        //myRigidBody.MovePosition(myRigidBody.position + movement * speed * Time.fixedDeltaTime);
       

    }



    public void move() {
      //  transform.position = transform.position + (Vector3)inputAxis.normalized * speed * Time.deltaTime;
    }

    public void decreaseSP() {
        this.playerSP.runtimeValue -= 5;
    }


    private void handleInput() {

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (movement != Vector2.zero) {
            myAnimator.SetFloat("moveX", movement.x);
            myAnimator.SetFloat("moveY", movement.y);
        }

        myAnimator.SetFloat("magnitude", movement.sqrMagnitude);

        if (Input.GetMouseButton(0)) {
           clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           Vector2 direction =  clickPoint - myRigidBody.position;
           myAnimator.SetFloat("attackX", direction.x);
           myAnimator.SetFloat("attackY", direction.y);
           myAnimator.SetBool("attacking", true);
        }
      
       

        /*Vector3 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = clickPoint - transform.position;
        direction.Normalize();
        myAnimator.SetFloat("attackX", direction.x);
        myAnimator.SetFloat("attackY", direction.y);
        myAnimator.SetBool("attacking", true);

    } else {

        inputAxis.x = Input.GetAxisRaw("Horizontal");
        inputAxis.y = Input.GetAxisRaw("Vertical");

        if (inputAxis != Vector2.zero) {
            myAnimator.SetFloat("moveX", inputAxis.x);
            myAnimator.SetFloat("moveY", inputAxis.y);

        }
        myAnimator.SetFloat("magnitude", inputAxis.magnitude);
    }*/

    }

    public override void OnGetKicked(int attack) {

        playerHP.runtimeValue -= attack;
    }


   

    //public enum State { idle,walk,attack,stagger};

    //private State playerState;
    //public int SP = 100;
    //private Vector2 change;


    //// Start is called before the first frame update
    //public override void Start() {
    //    base.Start();
    //    playerState = State.walk;
    //}

    //// Update is called once per frame
    //void Update() {


    //    change = Vector2.zero;
    //    change.x = Input.GetAxisRaw("Horizontal");
    //    change.y = Input.GetAxisRaw("Vertical");


    //    if (Input.GetKeyDown(KeyCode.LeftControl) && playerState != State.attack) {
    //            StartCoroutine(StaffKickCo());
    //            Debug.Log("Attack");

    //    } else if(playerState == State.walk || playerState == State.idle){

    //        if (change != Vector2.zero){

    //            myAnimator.SetFloat("moveX", change.x);
    //            myAnimator.SetFloat("moveY", change.y);
    //            myAnimator.SetBool("walking", true);
    //            change.Normalize();
    //            myRigidBody.MovePosition((Vector2)transform.position + change * speed * Time.deltaTime);
    //        }else{
    //            myAnimator.SetBool("walking", false);
    //        }
    //    }
    //}

    //IEnumerator StaffKickCo(){
    //    myAnimator.SetBool("attacking", true);
    //    playerState = State.attack;
    //    yield return null;

    //    myAnimator.SetBool("attacking", false);
    //    yield return new WaitForSeconds(0.2f);
    //    playerState = State.idle;
    //}
}