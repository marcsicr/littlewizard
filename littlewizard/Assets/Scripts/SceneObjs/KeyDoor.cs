using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    GameObject portal;
    Inventory inventory;
    Animator myAnimator;
    private void Awake() {

        portal = transform.Find("Portal").gameObject;
        portal.SetActive(false);
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        myAnimator = gameObject.GetComponent<Animator>();
    }
    public void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag(Player.TAG)) {
            
            if (inventory.useKey()) {
                myAnimator.SetBool("open", true);

                portal.SetActive(true);
            }
        }

        
    }
}
