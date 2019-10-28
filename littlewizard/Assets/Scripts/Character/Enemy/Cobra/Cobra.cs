using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobra : Enemy
{
    float nextAttack;
    public float minDistance = 2.5f;
    private float attackTimeout = 1f;
     public override void Start() {
        base.Start();
        nextAttack = Time.time + attackTimeout;
     }


    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.tag == "Player") {

            Player p = other.GetComponent<Player>();
            p.OnGetKicked(attackPower);
        }
    }


    public void attack() {

        if(Time.time > nextAttack) {

            myAnimator.SetBool("attack", true);
            nextAttack = 999f;
        }
    }

    public void attackEnd() {
        nextAttack = Time.time + attackTimeout;
    }
   

    public override void OnGetKicked(int attack) {

        //Debug.Log("Cobra kicked");
    }
}
