﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IdleState : PlayerState {
    /*Player can go from Idle->Attack or Idle->Walk*/
    public IdleState(Player player) : base(player) { }

    Vector2 movement;

    public override PlayerState handleInput() {

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");


        if (Input.GetMouseButtonDown(1)) {
            return new CastState(player, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }


        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && player.stamina.getRunTimeValue() > 0) {
            return new AttackState(player, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        if (movement != Vector2.zero) {
            player.walkState.setMovement(movement);
            return player.walkState;
        }

        return this;
    }
    public override void act() {

    }


}
