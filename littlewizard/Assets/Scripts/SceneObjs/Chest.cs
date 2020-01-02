using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : ItemContainer {

    public override Vector3 getThrowPoint() {
        return transform.Find("ThrowPoint").position;
    }

    public override void open() {
        myAnimator.SetTrigger("open");
    }
}
