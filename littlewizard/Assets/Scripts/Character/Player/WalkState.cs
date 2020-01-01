using UnityEngine;
using UnityEngine.EventSystems;

public class WalkState : PlayerState {

    public Vector2 movement;


    public WalkState(Player player) : base(player) {
        movement = Vector2.zero;

    }


    public override PlayerState handleInput() {
      
        PlayerState state = handleAttack();
        if (state != null)
            return state;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        

        if (movement != Vector2.zero) {

            Vector2 temp = movement.normalized;
            temp.x = Mathf.Round(temp.x);
            temp.y = Mathf.Round(temp.y);
            player.faceDirection = temp;
            return this;
        } else {

            playerAnimator.SetFloat("magnitude", 0);
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
            playerAnimator.SetFloat("magnitude", movement.sqrMagnitude);
            movement.Normalize();
            playerRB.MovePosition((Vector2)playerRB.position + movement * player.speed * Time.fixedDeltaTime);
        }

       // Debug.Log("Walking");
    }


}