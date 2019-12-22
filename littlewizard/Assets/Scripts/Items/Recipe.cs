using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe : Item {

    public Signal recipeCaught;

   
    public override void onItemCollect(Player player) {
        recipeCaught.Raise();
    }

}
