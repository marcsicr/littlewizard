using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastState : PlayerState {

    public bool castDone = false;
    private bool casting = false;
    public Vector3 castDirection;
    Vector2 movement;
    private Spell spell;
    public CastState(Player player,Vector3 worldPoint,Spell spell) : base(player) {

        castDirection = (worldPoint - player.transform.position).normalized;
        this.spell = spell;
    }
    public override void act() {

        if (!casting) {

           
            playerAnimator.SetFloat("moveX", castDirection.x);
            playerAnimator.SetFloat("moveY", castDirection.y);
            playerAnimator.SetTrigger("cast");

            //Instantiate bullet prefab and shot(castDirection);
           // castDone = true;
            player.StartCoroutine(CastCo(spell));
        }

        if (movement != Vector2.zero) {
            playerAnimator.SetFloat("moveX", movement.x);
            playerAnimator.SetFloat("moveY", movement.y);
            movement.Normalize();
            playerRB.MovePosition((Vector2)playerRB.position + movement * player.speed * Time.fixedDeltaTime);
        }

    }

    public IEnumerator CastCo(Spell spell) {
        casting = true;
        if(spell == Spell.BOLT) {

            //Instantiate bullet prefab and shot(castDirection);
            GameObject bullet = GameObject.Instantiate(player.boltPrefab, player.transform.position + castDirection.normalized, Quaternion.identity);
            yield return new WaitForSeconds(0.2f);
            
            bullet.GetComponent<Bullet>().shot(castDirection);
            castDone = true;
            yield break;
        }
       
        if(spell == Spell.SHIELD) {

            player.createShield();
            castDone = true;
        }
  
        
        
    }
    public override PlayerState handleInput() {

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (castDone) {
            return player.idleState;
        } else {
            return this;
        }
        
    }
}
