using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : ItemContainer {
    public override void open() {
        myAnimator.SetTrigger("open");
    }
}
