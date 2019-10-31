using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character{

    //States that can be reused
    public IdleState idleState; 
    public WalkState walkState; 

    public ObservableInt playerHP;
    public ObservableInt playerSP;
    public ObservableInt stamina;
   

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

            StartCoroutine(KickEffectCo());
            kickAnimation = false;
        }

    }

    public IEnumerator KickEffectCo() {
       
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
        //this.playerSP.runtimeValue -= 5;
    }

    public void decreaseStamina(int points) {
        int currentST = stamina.getRunTimeValue();
        stamina.UpdateValue(currentST - points);
    }


   /* private void handleInput() {

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
      

    }*/

    public override void OnGetKicked(int attack) {
        int hp = playerHP.getRunTimeValue();
        //Debug.Log("Player got kicked" + hp.ToString() + "Current HP" + hp.ToString()) ;
      
        kickAnimation = true;
        if(hp > 0) {
            if (hp > attack) {
                playerHP.UpdateValue(hp - attack);
            } else {
                playerHP.UpdateValue(0);
            }
        }

        kickAnimation = true;
    }


    public void OnCollectPotion(int points,PotionType type) {

        if (type == PotionType.HP) {
            usePotion(points, playerHP);
        } else if (type == PotionType.SP) {
            usePotion(points, playerSP);
        }

    }

    private void usePotion(int points, ObservableInt var) {

        int current = var.getRunTimeValue();
        int max = var.getInitialValue();
        if (current + points > max) {

            var.UpdateValue(max);
        } else {

            var.UpdateValue(current + points);
        }
    
    }
}