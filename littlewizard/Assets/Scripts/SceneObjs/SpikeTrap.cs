using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public bool timeOffset;
    public float time;
    public int damage;    
    Animator myAnimator;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
    }

    public void Start() {

        if (!timeOffset) {
            InvokeRepeating("changeState", 0f, time);
        } else {
            InvokeRepeating("changeState", time, time);
        }   
    }


    private void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag(Player.TAG)) {
            Player p = other.gameObject.GetComponent<Player>();
            p.OnGetKicked(damage);
        }
    }


    private void changeState() {
        bool currentState = myAnimator.GetBool("active");
        myAnimator.SetBool("active", !currentState);
    }
}
