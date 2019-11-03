using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : PlayerState
{
    public DieState(Player player) : base(player) { }
    public override void act() {
       
    }

    public override PlayerState handleInput() {

        return this;
    }
}
