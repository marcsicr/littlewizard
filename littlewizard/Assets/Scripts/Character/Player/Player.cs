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
    GameObject line;
    LineRenderer lr;


    public void Awake() {
        mat = gameObject.GetComponent<SpriteRenderer>().material;
         line = transform.Find("Line").gameObject;
         lr = line.GetComponent<LineRenderer>();

        
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



       // float width = Mathf.Abs(Mathf.Cos(Time.time));
        //lr.startWidth = width;
        //lr.endWidth = width;
        float offset = Time.time * 1.5f;

        
        lr.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, new Vector3(0, 0, -0.1f));

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


    public override void OnGetKicked(int attack) {
        int hp = playerHP.getRunTimeValue();
       
      
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