using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootAttack : MonoBehaviour
{
    private int attackPower = 1;

    private Animator myAnimator;

    public void Start() {
        myAnimator = gameObject.GetComponent<Animator>();
    }

    public void setAttackPower(int attackPower) {
        this.attackPower = attackPower;
    }

    public void attack(Transform t) {

        Vector3 dest = new Vector3(t.transform.position.x + 0.1f, t.transform.position.y + 0.1f);
        transform.position = dest;
        myAnimator.SetTrigger("attack");
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "Player") {

            Player p = other.gameObject.GetComponent<Player>();
            p.OnGetKicked(attackPower);
        }
    }
}
