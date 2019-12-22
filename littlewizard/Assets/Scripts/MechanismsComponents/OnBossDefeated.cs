using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBossDefeated : Activator {
    
    
   public void OnBossIsDefeated() {

        state = true;
        mechanism.notifyStatusChange(this);

   }

    public override void Reset() {
        //throw new System.NotImplementedException();
    }
}
