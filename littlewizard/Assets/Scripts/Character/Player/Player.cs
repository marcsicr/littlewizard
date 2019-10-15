using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character{

    public FloatVar playerHP;
    public FloatVar playerSP;
    
    PlayerState currentState;
    public WalkState walkState;
    public IdleState idleState;

    private Vector2 inputAxis;
    private Vector2 attackAxis;
    public override void Start() {
        base.Start();
       // walkState = new WalkState(this);
       //idleState = new IdleState(this);
       // currentState = idleState;

    }

   

    public void Update() {
     
        handleInput();
        // currentState = currentState.handleInput();
       // currentState.act();
    }
  

    public void setOrientation(float posX, float posY) {


        myAnimator.SetFloat("moveX", posX);
        myAnimator.SetFloat("moveY", posY);
    }

    public void move() {
        transform.position = transform.position + (Vector3)inputAxis.normalized * speed * Time.deltaTime;
    }

    public void decreaseSP() {

        this.playerSP.runtimeValue -= 5f;
    }


    private void handleInput() {


        if (Input.GetMouseButtonDown(0)) {

            Vector3 clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = clickPoint - transform.position;
            direction.Normalize();
            myAnimator.SetFloat("attackX", direction.x);
            myAnimator.SetFloat("attackY", direction.y);
            myAnimator.SetBool("attacking", true);

        }

        inputAxis.x = Input.GetAxisRaw("Horizontal");
        inputAxis.y = Input.GetAxisRaw("Vertical");

       if(inputAxis != Vector2.zero) {
            myAnimator.SetFloat("inputX", inputAxis.x);
            myAnimator.SetFloat("inputY", inputAxis.y);
          
        }
        myAnimator.SetFloat("magnitude", inputAxis.magnitude);






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