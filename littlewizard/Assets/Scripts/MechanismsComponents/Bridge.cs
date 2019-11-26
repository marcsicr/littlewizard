using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : Activable
{
    Animator myAnimator;

    protected override void Start() {

        base.Start();
        myAnimator = GetComponent<Animator>();
    }

    public override void Activate() {
        myAnimator.SetBool("active", true);
    }

    public override void Desactivate() {
        myAnimator.SetBool("active", false);
    }
}
