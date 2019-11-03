using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinS : Enemy
{
   
    // Start is called before the first frame update
     public override void Start(){
        base.Start();
    }

  
   

    public override void OnGetKicked(int attack) {

        base.OnGetKicked(attack);
        this.HP -= attack;
        if(this.HP <= 0) {

            this.myAnimator.SetTrigger("die");
        }
        bar.updateBar(HP);
    }


    protected override void attackAction() {
        myAnimator.SetBool("attack", true);
    }
}
