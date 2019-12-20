using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingElementStopper : Activable
{
    public override void Activate() {
        GetComponent<MovingElement>().isPaused(true);
    }

    public override void Desactivate() {
        GetComponent<MovingElement>().isPaused(false);
    }

}
