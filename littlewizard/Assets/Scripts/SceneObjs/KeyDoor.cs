﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    public PortalTransfer portal;
    public AudioClip openClip;

    Animator myAnimator;
   
    private void Awake() {

        portal.gameObject.SetActive(false);
        myAnimator = gameObject.GetComponent<Animator>();
    }
    public void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag(Player.TAG)) {
            
            if (LevelManager.Instance.useKey()) {

                SoundManager.Instance.playEffect(openClip);
                myAnimator.SetBool("open", true);

                portal.gameObject.SetActive(true);
            }
        }

        
    }
}
