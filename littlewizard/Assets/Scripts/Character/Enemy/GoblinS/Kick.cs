using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.CompareTag(Player.TAG)){

            Player player = other.GetComponent<Player>();
            Enemy enemy = this.gameObject.GetComponentInParent<Enemy>();
            player.OnGetKicked(enemy.attackPower);

        }
    }
}
