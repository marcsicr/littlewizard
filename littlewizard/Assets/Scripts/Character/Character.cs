using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour{
    public float speed = 2f;
    public bool debugCharacter = false;
    
    protected Rigidbody2D myRigidBody;
    protected Animator myAnimator;
    protected Material mat;


    
    protected bool isFlashing;
    protected bool kickAnimation;
    private float flashSpeed = 4f;


    public virtual void Start() {

        //Debug.Log("Character");

        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

        //Set initial orientation
        myAnimator.SetFloat("moveX", 0.0f);
        myAnimator.SetFloat("moveY", -1.0f);

        mat = gameObject.GetComponent<SpriteRenderer>().material;
        
    }

    public abstract void onTransferEnter();
    public abstract void onTransferLeave();

    public abstract void OnGetKicked(int attack);

    public virtual void Update() {

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
}
