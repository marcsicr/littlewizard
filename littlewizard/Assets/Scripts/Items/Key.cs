﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item {
    public override void onItemCollect(Player player) {
        Debug.Log("Key Collected");
    }
}