using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    float dissapearTime;
    
    private Animator myAnimator;
    private Player player;
    bool active;
    private void Awake() {
        myAnimator = GetComponent<Animator>();
        active = false;
        player = transform.parent.GetComponent<Player>();
        
    }

    public void create(float duration) {

        if (!active) {
            active = true;
            myAnimator.SetTrigger("create");
            player.setInvencible(true);
            dissapearTime = Time.time + duration;
            
        }
        
    }

    private void Update() {

        if (active && Time.time > dissapearTime) {

            dissapear();
        }
    }

    public void dissapear() {

        myAnimator.SetTrigger("dissapear");
        active = false;
        player.setInvencible(false);
        //Reactivate player hit collider
    }
    
}
