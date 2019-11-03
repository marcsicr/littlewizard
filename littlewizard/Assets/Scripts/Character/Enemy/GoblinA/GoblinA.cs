using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinA : Enemy
{
  private enum GolbinAState { patrol,attack,idle}

    public Vector2[] patrolPoints;

    // Update is called once per frame
    void Update(){

        

        if (isPlayerInAttackRadius()) {

            attackAtempt();
        }
    }

    public override void OnGetKicked(int attack) {
        Debug.Log("Enemy Kicked");
    }

    protected override void attackAction() {

        Vector2 direction = getTargetDirection();
        myAnimator.SetFloat("moveX",direction.x);
        myAnimator.SetFloat("moveY", direction.y);
        myAnimator.SetTrigger("shot");
    }
}
