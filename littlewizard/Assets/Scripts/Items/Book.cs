using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : Item {

    public Spell spell;
    public override void onItemCollect(Player player) {

        LevelManager.Instance.spellLvlUp(spell);
    }
}
