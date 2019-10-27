using UnityEngine;
using UnityEngine.EventSystems;

public class WalkState : PlayerState {

    public Vector2 movement;


    public WalkState(Player player) : base(player) {
        movement = Vector2.zero;

    }


    public override PlayerState handleInput() {

        //Check first if we should change to another state
        if (Input.GetMouseButtonDown(0) &&!EventSystem.current.IsPointerOverGameObject()) { //If left mouse click && pointer is not over some UI element
            playerAnimator.SetFloat("magnitude", 0); //Disable walking animation before going to attack
            return new AttackState(player, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        playerAnimator.SetFloat("magnitude", movement.sqrMagnitude);

        if (movement != Vector2.zero) {
            return this;
        } else {
            return player.idleState;
        }
    }

    public void setMovement(Vector2 movement) {
        this.movement = movement;
    }

    public override void act() {

        if (movement != Vector2.zero) {
            playerAnimator.SetFloat("moveX", movement.x);
            playerAnimator.SetFloat("moveY", movement.y);
            movement.Normalize();
            playerRB.MovePosition((Vector2)playerRB.position + movement * player.speed * Time.fixedDeltaTime);
        }
    }


}