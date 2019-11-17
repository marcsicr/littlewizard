using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour {
    Animator myAnimator;
    public Item throwItem;
    void Start() {
        myAnimator = GetComponent<Animator>();
    }

   public void destroy() {

        myAnimator.SetTrigger("destroy");
        throwLoot();
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(gameObject, 1.5f);
   }

   private void throwLoot() {

       if(throwItem != null) {
            GameObject.Instantiate(throwItem, transform.position, Quaternion.identity);
        }
   }


}
