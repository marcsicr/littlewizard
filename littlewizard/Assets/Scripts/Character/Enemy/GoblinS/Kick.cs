using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kick : MonoBehaviour {
    public AudioClip kickClip;
    public void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.CompareTag(Player.TAG)){

            Player player = other.GetComponent<Player>();
            Enemy enemy = GetComponentInParent<Enemy>();
            SoundManager.Instance.playEffect(kickClip);
            player.OnGetKicked(enemy.attackPower);

        }
    }
}
