using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBook : Item
{
    public ObservableInt levelBolt;

    public override void onItemCollect(Player player) {

        levelBolt.UpdateValue(levelBolt.getRunTimeValue() + 1);
        Debug.Log("Book collected");
    }
}
