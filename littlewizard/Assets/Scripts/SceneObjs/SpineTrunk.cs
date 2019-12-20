using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineTrunk : MonoBehaviour
{
    public int damage;

    private void OnCollisionStay2D(Collision2D other) {

            if (other.gameObject.CompareTag(Player.TAG)) {

                Player p = other.gameObject.GetComponent<Player>();
                p.OnGetKicked(damage);
                p.removeShield();
            }
        
    }
}
