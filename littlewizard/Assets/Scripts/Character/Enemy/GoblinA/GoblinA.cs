using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinA : Enemy
{
  private enum GolbinAState { patrol,attack,idle}

    public Vector2[] patrolPoints;
    public GameObject arrowPrefab;
   

    public override void OnGetKicked(int attack) {
        base.OnGetKicked(attack);
        this.HP -= attack;
        if (this.HP <= 0) {

            Destroy(gameObject);
        }

        bar.updateBar(HP);
    }

    protected override void attackAction() {

        Vector2 direction = getTargetDirection();
        myAnimator.SetFloat("moveX",direction.x);
        myAnimator.SetFloat("moveY", direction.y);
        myAnimator.SetTrigger("shot");


        StartCoroutine(waitShotCo());
      
    }

    public IEnumerator waitShotCo() {
        Vector2 direction = getTargetDirection();
        yield return new WaitForSeconds(0.1f);
        GameObject arrow = Instantiate(arrowPrefab, new Vector3(transform.position.x + direction.x, transform.position.y + direction.y, transform.position.z), Quaternion.identity);
        arrow.GetComponent<Arrow>().shot(direction);
        //arrow.shot();
    }
}
