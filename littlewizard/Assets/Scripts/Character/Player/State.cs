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

    protected PlayerState handleAttack() {

        if (player.showingAlertBubble)
            return null;

        if (Input.GetKeyUp(KeyCode.Space)) {
      
            Vector2 point = (Vector2)player.getPlayerCastPoint() + player.faceDirection;

            Spell s = player.getActiveSpell();
            if (s != Spell.NONE) {

                int spNeeded = SpellsManager.Instance.computeSPConsumed(s);

                if (spNeeded < player.playerSP.getRunTimeValue()){
                    return new CastState(player, point, s, Vector2.zero);
                }
                
            }

            //If no spell is selected try to hit with staff
            if (player.stamina.getRunTimeValue() > 0) {
                return new AttackState(player, player.faceDirection);
            }
        }

        return null;
    }

    public abstract void act();
    
}
