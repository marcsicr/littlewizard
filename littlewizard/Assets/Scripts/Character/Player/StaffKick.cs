﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffKick : MonoBehaviour
{
    public int KickPower;
    public AudioClip[] hitClips;
   
    public void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.tag == Enemy.TAG) {
            AbstractEnemy enemy = other.gameObject.GetComponent<AbstractEnemy>();
            enemy.OnGetKicked(KickPower);
            
        }else if (other.CompareTag(ItemContainer.TAG)) {

            ItemContainer container = other.GetComponent<ItemContainer>();
            container.open();
        }
    }
}
