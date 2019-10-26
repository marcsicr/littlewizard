using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Item
{
    public IntVar gemsOnLevel;
    public IntVar gemsCaught;
   
    public void Awake() {
        gemsOnLevel.runtimeValue += 1;
        
    }

    public override void onItemCollect(Player player) {
        Debug.Log("Player collided with gem");
        gemsCaught.runtimeValue++;
    }
}
