using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    Inventory inventory;
    Animator myAnimator;
    private void Awake() {

        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        myAnimator = gameObject.GetComponent<Animator>();
    }
    public void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Player")) {
            
            if (inventory.useKey()) {
                myAnimator.SetBool("open", true);
            }
        }

        
    }
}
