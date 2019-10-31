using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinS : Enemy
{
   
    // Start is called before the first frame update
     public override void Start(){
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnGetKicked(int attack) {

    
        this.HP -= attack;
        if(this.HP <= 0) {

            this.myAnimator.SetTrigger("die");
        }
        bar.updateBar(HP);
    }

    public override int GetHashCode() {
        return base.GetHashCode();
    }

    protected override void attackAction() {

        myAnimator.SetBool("attack", true);
    }
}
