﻿
using System.Collections;
using UnityEngine;

public class Player : Character{

    RuntimeAnimatorController playerRAC;

    [HideInInspector]
    public static readonly string TAG = "Player"; //This tag must be defined first on inspector

    [HideInInspector]
    public Vector2 faceDirection;

    //States that can be reused
    public IdleState idleState; 
    public WalkState walkState;
    public DieState dieState;

    public PlayerState currentState;

    public ObservableInteger playerHP;
    public ObservableInteger playerSP;
    public ObservableInteger stamina;
   


    public SpellSignal spellCasted;

    public AudioClip[] castClips;
    public AudioClip[] damagedClips;
    public AudioClip[] staffKickClips;
    public AudioClip[] healClips;

    private Shield shield;
    private bool isInvencible;
    
    [HideInInspector]
    public bool showingAlertBubble = false;

    public bool startOnPortal = false;

    private GameObject stunnedEffect;

    private float nextSTRecup;
    private float recuPIntervalST = 1f;

    private float nextSPRecup;
    private float recuPIntervalSP = 1f;

    
    private bool freeze = true;

    private float lastKick = 0;

    public GameObject boltPrefab;
    public GameObject rayAttkPrefab;

    public GameObject levelPortalPrefab;

    public Color flashColor;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;



    public void Awake() {
        mat = gameObject.GetComponent<SpriteRenderer>().material;
        mat.SetVector("_FlashColor", flashColor);

        shield = GameObject.Find("Shield").GetComponent<Shield>();
        if (shield == null)
            Debug.Log("Shield is null");
        isInvencible = false;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;

        stunnedEffect = GameObject.Find("StunnedEffect");
        stunnedEffect.SetActive(false);
        
    }


    public override void Start() {
        base.Start();
        
        
        nextSTRecup = Time.time + recuPIntervalST;
        nextSPRecup = Time.time + recuPIntervalSP;
       
        idleState = new IdleState(this);
        walkState = new WalkState(this);
        dieState = new DieState(this);
        currentState = idleState;

        if (startOnPortal) {
            StartCoroutine(PlayerAppearCo());
        } else {
            spriteRenderer.enabled = true;
            freeze = false;
            boxCollider.enabled = true;
        }

        
    }

    private IEnumerator PlayerAppearCo(){

        Vector3 portalPos = transform.position;
        portalPos.y += 0.2f; //Center portal on player sprite position

        GameObject portalInstance = Instantiate(levelPortalPrefab, portalPos, Quaternion.identity, null);
        LevelPortal portal = portalInstance.GetComponent<LevelPortal>();
        portal.appear(0,false);
        yield return new WaitForSeconds(1);
       
        transform.localScale = Vector3.zero;
        spriteRenderer.enabled = true;

        mat.SetVector("_FlashColor", Color.white);
        for (float scale = 0; scale < 1; scale += 0.1f){

            mat.SetFloat("_FlashAmount", 1-scale);
            transform.localScale = new Vector3(scale, scale, 0);
            yield return null;
        }
        mat.SetFloat("_FlashAmount", 0);
        mat.SetVector("_FlashColor", flashColor);
        portal.disappear();
        yield return null;

        Destroy(portal.gameObject, 1);
        yield return new WaitForSeconds(0.5f);

        freeze = false;
        boxCollider.enabled = true;
       
    }

    private IEnumerator PlayerDissapearCo() {

        mat.SetVector("_FlashColor", Color.white);
        for (float scale = 0; scale < 1; scale += 0.1f) {

            mat.SetFloat("_FlashAmount", 1 - scale);
            transform.localScale = new Vector3(1-scale, 1-scale, 0);
            yield return null;
        }

        spriteRenderer.enabled = false;
    }

    public void createShield(float duration) {
        shield.create(duration);
    }

    public void removeShield() {
        shield.dissapear();
    }

    public Animator getAnimator() {

        return this.myAnimator;
    }

    public override void Update() {

        base.Update();

        if (!freeze) {
            currentState = currentState.handleInput();
        }
    }

    public void FixedUpdate() {

        if (!freeze) {
            currentState.act();
        }

        if (Time.time > nextSTRecup) {
            if (stamina.getRunTimeValue() < stamina.getInitialValue()) {
                stamina.UpdateValue(stamina.getRunTimeValue() + 1);
            }
            nextSTRecup = Time.time + recuPIntervalST;
        }

        if (Time.time > nextSPRecup) {
            if (playerSP.getRunTimeValue() < playerSP.getInitialValue()) {
                playerSP.UpdateValue(playerSP.getRunTimeValue() + 1);
            }

            nextSPRecup = Time.time + recuPIntervalSP;
        }

    }




    public void onTimelineStart() {
        /*playerRAC = GetComponent<Animator>().runtimeAnimatorController;
        GetComponent<Animator>().runtimeAnimatorController = null;*/
        freeze = true;
    }

    public void onTimelineEnd() {
        freeze = false;
        /* myAnimator.runtimeAnimatorController = playerRAC;*/
    }


    public Vector2 movingDirection() {

        return new Vector2(myAnimator.GetFloat("moveX"), myAnimator.GetFloat("moveY"));
    }
   
 
   

    public Spell getActiveSpell() {

        return SpellsManager.Instance.selectedSpell;
        //return LevelManager.Instance.selectedSpell;

    }
   

    /*Set if enemies can hurt player*/
    public void setInvencible(bool isInvencible) {
        this.isInvencible = isInvencible;
    }

    public void decreaseStamina(int points) {
        int currentST = stamina.getRunTimeValue();
        stamina.UpdateValue(currentST - points);
    }

    public void stun(float timeOut) {

        StartCoroutine(stunCo(timeOut));
    }

    private IEnumerator stunCo(float timeOut) {

        freeze = true;
        myAnimator.SetFloat("magnitude", 0);
        yield return new WaitForSeconds(0.1f);
        stunnedEffect.SetActive(true);

        while(timeOut > 0) {
            timeOut -= Time.deltaTime;
            myRigidBody.velocity = Vector2.zero;
            yield return null;
        }

        freeze = false;
        stunnedEffect.SetActive(false);
    }


    public override void OnGetKicked(int attack) {

        if (isInvencible)
            return;

        kickAnimation = true;
        if(playerHP.getRunTimeValue() > 0) {
            if (playerHP.getRunTimeValue() > attack) {
                playerHP.UpdateValue(playerHP.getRunTimeValue() - attack);

                int randomIndex = Random.Range(0, damagedClips.Length);

                if (Time.time > lastKick + 0.25f) {
                    SoundManager.Instance.playVoice(damagedClips[randomIndex]);
                    lastKick = Time.time;
                } 
                

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
    }

    public void showAlertBubble(bool show) {

        showingAlertBubble = show;
        transform.Find("AlertBubble").gameObject.SetActive(show);
    }


    public void OnCollectPotion(int points,PotionType type) {

        if (type == PotionType.HP) {
            usePotion(points, playerHP);
        } else if (type == PotionType.SP) {
            usePotion(points, playerSP);
        }

       
        Invoke("playHealClip", 0.5f);
    }

    private void playHealClip() {
        int randomIndex = Random.Range(0, healClips.Length);
        SoundManager.Instance.playEffect(healClips[randomIndex]);
    }

    private void usePotion(int points, ObservableInteger var) {

        int current = var.getRunTimeValue();
        int max = var.getInitialValue();
        if (current + points > max) {

            var.UpdateValue(max);
        } else {

            var.UpdateValue(current + points);
        }
    
    }

    public override void onTransferEnter() {
        freeze = true;
        myAnimator.SetFloat("magnitude", 0);
        
    }

    public override void onTransferLeave() {
        
        StartCoroutine(defrostCo());
    }

    public IEnumerator defrostCo() {

        yield return new WaitForSeconds(0.9f);
        freeze = false;
    }


    public void die() {
        SpriteRenderer r = gameObject.GetComponent<SpriteRenderer>();
        BoxCollider2D b = gameObject.GetComponent<BoxCollider2D>();
        r.enabled = false;
        b.enabled = false;
        currentState = dieState;
        myRigidBody.velocity = Vector2.zero;
        myRigidBody.angularVelocity = 0;

        playerHP.reset();
        playerSP.reset();
        stamina.reset();
    }

    public Vector3 getCollisionCenterPoint() {

        Vector3 pos = transform.position;
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        pos.y += col.offset.y - col.size.y/2;
        return pos;
    }

  
    public Vector3 getPlayerCastPoint() {

        Vector3 pos = transform.position;
        pos.x += 0.1f;
        pos.y -= 0.22f;

        return pos;
    }


    public void dissapear() {

        freeze = true;
        boxCollider.enabled = false;
        StartCoroutine(PlayerDissapearCo());
        
   
    }

    public override int getMapHeight() {

        Vector3 pos = getCollisionCenterPoint();
        return LevelManager.Instance.getTileLevel(pos);
    }

    public static Player GetPlayer() {

       return GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
}