using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float duration;
    float dissapearTime;
    private Animator myAnimator;
    bool active;
    private void Awake() {
        myAnimator = GetComponent<Animator>();
        active = false;
    }

    public void create() {

        if (!active) {
            myAnimator.SetTrigger("create");
            dissapearTime = Time.time + duration;
            active = true;
        }
        
    }

    private void Update() {

        if (active && Time.time > dissapearTime) {

            dissapear();
        }
    }

    private void dissapear() {

        myAnimator.SetTrigger("dissapear");
        active = false;
    }
    
}
