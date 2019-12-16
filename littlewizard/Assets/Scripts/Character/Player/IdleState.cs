using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IdleState : PlayerState {
    /*Player can go from Idle->Attack or Idle->Walk*/
    public IdleState(Player player) : base(player) { linePointer = player.transform.Find("LinePointer").GetComponent<LinePointer>(); }


    LinePointer linePointer;
    private bool showingLine = false;
    Vector2 movement;
    Vector2 lineDirection;
    public bool spaceDown = false;
    public float timeDown = 0;
    public override PlayerState handleInput() {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        handleLinePointer();
      
        PlayerState attackState = handleAttack();
        if (attackState != null )
            return attackState;


        if (movement != Vector2.zero && !showingLine) {
            player.walkState.setMovement(movement);
            return player.walkState;
        }

        return this;
    }

    public void handleLinePointer() {

        if (Input.GetKey(KeyCode.Space)) {
            timeDown += Time.deltaTime;
        } else {
            timeDown = 0;
            linePointer.hideLine();
            showingLine = false;
        }

        if (timeDown > 0.3f && !showingLine && LevelManager.Instance.selectedSpell == Spell.BOLT) {
            lineDirection = player.faceDirection;
            linePointer.show(player.getPlayerCastPoint(), lineDirection);
            showingLine = true;
        }

        if (showingLine) { //Move line acording to player input

            float degrees;
            if (movement.x != 0 || movement.y !=0) {
                if (movement.x > 0 || movement.y <0) {
                    degrees = -1;
                } else {
                    degrees = 1;
                }
                lineDirection = Quaternion.AngleAxis(degrees, Vector3.forward) * lineDirection;

                linePointer.show(player.getPlayerCastPoint(), lineDirection);
                player.getAnimator().SetFloat("moveX", lineDirection.x);
                player.getAnimator().SetFloat("moveY", lineDirection.y);
                player.faceDirection = lineDirection;

            }
        }
    }

    public override void act() {

        playerAnimator.SetFloat("magnitude", 0);
        playerRB.velocity = Vector2.zero;
        playerRB.angularVelocity = 0;
    }
}
