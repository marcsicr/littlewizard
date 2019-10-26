using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PotionType { HP,SP};
public class Potion : Item
{
    public int points;
    public PotionType type;

    public override void onItemCollect(Player player) {
        player.OnCollectPotion(points, type);
    }
}
