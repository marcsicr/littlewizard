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
    private bool kickAnimation = false;
    bool isFlashing = false;
    private Material mat;
    private float flashSpeed = 4f;
    public void Awake() {
        mat = gameObject.GetComponent<SpriteRenderer>().material;
            
    }
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

        if (kickAnimation) {

            StartCoroutine(KikEffectCo());
            kickAnimation = false;
        }

    }

    public IEnumerator KikEffectCo() {
       
            isFlashing = false;
            yield return new WaitForEndOfFrame();
            isFlashing = true;
            float flash = 1f;
            while (isFlashing && flash >= 0) {
                flash -= Time.deltaTime * flashSpeed;
                mat.SetFloat("_FlashAmount", flash);
                yield return null;
            }
            isFlashing = false;
        
    }


    public void FixedUpdate() {

        currentState.act();
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
      

    }

    public override void OnGetKicked(int attack) {
        Debug.Log("Player got kicked" + playerHP.runtimeValue.ToString());
        kickAnimation = true;
        if(playerHP.runtimeValue > 0) {

            if (playerHP.runtimeValue > attack) {

                playerHP.runtimeValue -= attack;
            } else {
                playerHP.runtimeValue = 0;
            }
        }

        SpriteRenderer render = gameObject.GetComponent<SpriteRenderer>();
        render.color = Color.white;

        kickAnimation = true;
       
    }

}