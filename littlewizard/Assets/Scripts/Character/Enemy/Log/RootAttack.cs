﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootAttack : MonoBehaviour
{
    private int attackPower = 1;
    public AudioClip hitClip;
    private Animator myAnimator;

    public void Start() {
        myAnimator = gameObject.GetComponent<Animator>();
    }

    public void setAttackPower(int attackPower) {
        this.attackPower = attackPower;
    }

    public void attack(Vector3 position) {

        Vector2 random = Random.insideUnitCircle;
        Vector3 dest = new Vector3(position.x + random.x, position.y + random.y);
        transform.position = dest;
        myAnimator.SetTrigger("attack");
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "Player") {

            SoundManager.Instance.playEffect(hitClip);
            Player p = other.gameObject.GetComponent<Player>();
            p.OnGetKicked(attackPower);
        }
    }
}
