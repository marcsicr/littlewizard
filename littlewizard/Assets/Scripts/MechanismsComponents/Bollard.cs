﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bollard : Activable {

    Animator myAnimator;

    protected override void Start() {

        base.Start();
        myAnimator = GetComponent<Animator>();
    }
    public override void Activate() {
        myAnimator.SetBool("activate", true);
    }

    public override void Desactivate() {
        myAnimator.SetBool("activate", false);
    }
}