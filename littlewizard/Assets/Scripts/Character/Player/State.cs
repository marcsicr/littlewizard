using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected Player player;
    protected Animator playerAnimator;
    protected Rigidbody2D playerRB;

    protected PlayerState(Player player) {
        this.player = player;
        playerAnimator = player.GetComponent<Animator>();
        playerRB = player.GetComponent<Rigidbody2D>();
    }
    
    public abstract PlayerState handleInput();
    public abstract void act();
    
}
