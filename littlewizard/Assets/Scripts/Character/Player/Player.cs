using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Character{

    //States that can be reused
    public IdleState idleState; 
    public WalkState walkState;
    public DieState dieState;

    private PlayerState currentState;

    public ObservableInt playerHP;
    public ObservableInt playerSP;
    public ObservableInt stamina;
    public Signal gameOverSignal;

    private float nextSTRecup;
    private float recuPInterval = 2f;

    public GameObject boltPrefab;

    public void Awake() {
        mat = gameObject.GetComponent<SpriteRenderer>().material;
    }


    public override void Start() {
        base.Start();

        nextSTRecup = Time.time + recuPInterval;
       
        idleState = new IdleState(this);
        walkState = new WalkState(this);
        dieState = new DieState(this);
        currentState = idleState;
        
    }

   public Animator getAnimator() {

        return this.myAnimator;
    }

    public override void Update() {

        base.Update();
        currentState = currentState.handleInput();

    }



    public Vector2 movingDirection() {

        return new Vector2(myAnimator.GetFloat("moveX"), myAnimator.GetFloat("moveY"));
    }
   
 
    public void FixedUpdate() {

        currentState.act();
        if(Time.time > nextSTRecup) {
            if(stamina.getRunTimeValue() < stamina.getInitialValue()) {
                stamina.UpdateValue(stamina.getRunTimeValue() + 1);
            }

            nextSTRecup = Time.time + recuPInterval;
        }
    }


    public void decreaseSP() {
        //this.playerSP.runtimeValue -= 5;
    }

    
    public void decreaseStamina(int points) {
        int currentST = stamina.getRunTimeValue();
        stamina.UpdateValue(currentST - points);
    }


    public override void OnGetKicked(int attack) {
     
       
      
        kickAnimation = true;
        if(playerHP.getRunTimeValue() > 0) {
            if (playerHP.getRunTimeValue() > attack) {
                playerHP.UpdateValue(playerHP.getRunTimeValue() - attack);
            } else {
                playerHP.UpdateValue(0);
            }
        }

        if(playerHP.getRunTimeValue() <= 0) {

            GameObject dieExplosion = transform.Find("dieExplosion").gameObject;
            dieExplosion.SetActive(true);
        } else {

            kickAnimation = true;
        }

        //Debug.Log("HP;" + playerHP.getRunTimeValue());
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

    
    public void die() {
        SpriteRenderer r = gameObject.GetComponent<SpriteRenderer>();
        BoxCollider2D b = gameObject.GetComponent<BoxCollider2D>();
        r.enabled = false;
        b.enabled = false;
        currentState = dieState;
        

        StartCoroutine(gameOverTimeoutCo());
    }

    public IEnumerator gameOverTimeoutCo() {

        playerHP.reset();
        playerSP.reset();
        stamina.reset();
        yield return new WaitForSeconds(1);
        gameOverSignal.Raise(); //Show game over UI
    }
}