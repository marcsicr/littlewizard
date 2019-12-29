using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineTrunk : MonoBehaviour {
    public int damage;
    float timeout;
    static float kickInterval = 0.5f;
    private bool isIn;

    private void OnCollisionEnter2D(Collision2D other) {

        if (other.gameObject.CompareTag(Player.TAG)) {

            Player p = other.gameObject.GetComponent<Player>();
            p.OnGetKicked(damage);
            p.removeShield();

            timeout = kickInterval;
            isIn = true;
        }
    }
    private void OnCollisionStay2D(Collision2D other) {

        timeout -= Time.fixedDeltaTime;
        if (timeout <= 0 && isIn) {

            if (other.gameObject.CompareTag(Player.TAG)) {

                Player p = other.gameObject.GetComponent<Player>();
                p.OnGetKicked(damage);

                timeout = kickInterval;
            }
            
        }
    }

    private void OnCollisionExit2D(Collision2D other) {

        if (other.gameObject.CompareTag(Player.TAG)) {

            isIn = false;
        }
    }
}
