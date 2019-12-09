using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : ItemContainer {
  
   public override void open() {

        myAnimator.SetTrigger("destroy");
        //throwLoot();
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(gameObject, 1.5f);
   }

}
