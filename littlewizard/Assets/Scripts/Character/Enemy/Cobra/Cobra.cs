using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cobra : Enemy
{
    
    //public float minDistance = 2.5f;
    
     public override void Start() {
        base.Start();
     }


    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.tag == "Player") {

            Player p = other.GetComponent<Player>();
            p.OnGetKicked(attackPower);
        }
    }



    public override void OnGetKicked(int attack) {
        base.OnGetKicked(attack);
        this.HP -= attack;
        if (this.HP <= 0) {

            Destroy(gameObject);
        }

        bar.updateBar(HP);
    }

    protected override void attackAction() {
        myAnimator.SetBool("attack", true);
    }
}
