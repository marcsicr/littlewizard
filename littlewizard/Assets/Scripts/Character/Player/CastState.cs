using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastState : PlayerState {

    public bool castDone = false;
    private bool casting = false;
    public Vector2 castDirection;
    Vector2 movement;
    private Spell spell;
    public CastState(Player player,Vector3 worldPoint,Spell spell) : base(player) {

        castDirection = (worldPoint - player.transform.position);
        castDirection.Normalize();
        this.spell = spell;
    }
    public override void act() {

        playerAnimator.SetFloat("moveX", castDirection.x);
        playerAnimator.SetFloat("moveY", castDirection.y);
        if (!casting) {

            playerAnimator.SetTrigger("cast");

            //Instantiate bullet prefab and shot(castDirection);
            player.StartCoroutine(CastCo(spell));
        }

        if (movement != Vector2.zero) {
            playerAnimator.SetFloat("moveX", movement.x);
            playerAnimator.SetFloat("moveY", movement.y);
            movement.Normalize();
            playerAnimator.SetFloat("magnitude", movement.sqrMagnitude);
            playerRB.MovePosition((Vector2)playerRB.position + movement * player.speed * Time.fixedDeltaTime);
        }

       // Debug.Log("Cast state");
    }

    public IEnumerator CastCo(Spell spell) {
        casting = true;
        if(spell == Spell.BOLT) {

            //Instantiate bullet prefab and shot(castDirection);
            GameObject bullet = GameObject.Instantiate(player.boltPrefab, player.transform.position, Quaternion.identity);
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), bullet.GetComponent<Collider2D>());
            yield return new WaitForSeconds(0.2f);

            bullet.GetComponent<Bullet>().shot(castDirection);
            castDone = true;
            player.boltCasted.Raise();
            yield break;
        }
       
        if(spell == Spell.SHIELD) {

            player.createShield();
            player.shieldCasted.Raise();
            castDone = true;
            yield break;
        }

        if(spell == Spell.RANGE_ATTACK) {

            GameObject rayAttack = GameObject.Instantiate(player.rayAttkPrefab, player.transform.position, Quaternion.identity);
            RangeAttk ray = rayAttack.GetComponent<RangeAttk>();

            ray.shot(castDirection);

            player.rangeAtkCasted.Raise();
            castDone = true;
            yield break;
        }

        castDone = true;
        
    }
    public override PlayerState handleInput() {

      
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        playerAnimator.SetFloat("magnitude", movement.sqrMagnitude);

       // Debug.Log("H:" + Input.GetAxisRaw("Horizontal") + "V:" + Input.GetAxisRaw("Vertical") + " castDone:" + castDone.ToString());
        
        
        if (castDone && movement.sqrMagnitude < 0.1f) {
            return player.idleState;
        } else if (castDone && movement.sqrMagnitude >=0.1f){
            return player.walkState;
        } else {

            //Debug.Log("Input CastState returning this");
            return this;
        }
        
    }
}
