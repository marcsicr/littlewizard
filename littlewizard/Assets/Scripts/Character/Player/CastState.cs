using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastState : PlayerState {

    public bool castDone = false;
    private bool casting = false;

    public Vector3 worldPoint;
    public Vector2 castDirection;
    Vector2 movement;
    private Spell spell;
    //public CastState(Player player,Vector3 worldPoint,Spell spell,Vector2 movement) : base(player) {

    //    castDirection = (worldPoint - player.transform.position);
    //    castDirection.Normalize();
    //    this.worldPoint = worldPoint;
    //    this.movement = movement;
    //    this.spell = spell;
    //}

    public CastState(Player player,Vector3 worldPoint,Spell spell,Vector2 movement) : base(player) {

       castDirection = (worldPoint - player.getPlayerCastPoint());
       castDirection.Normalize();
       this.worldPoint = worldPoint;
       this.movement = movement;
        this.spell = spell;
    }
    public override void act() {

        playerAnimator.SetFloat("moveX", castDirection.x);
        playerAnimator.SetFloat("moveY", castDirection.y);

        if (movement != Vector2.zero) {
            playerAnimator.SetFloat("moveX", movement.x);
            playerAnimator.SetFloat("moveY", movement.y);
            movement.Normalize();
            playerAnimator.SetFloat("magnitude", movement.sqrMagnitude);
            playerRB.MovePosition((Vector2)playerRB.position + movement * player.speed * Time.fixedDeltaTime);
        }

        if (!casting) {

            
            playerAnimator.SetTrigger("cast");
            player.StartCoroutine(CastCo(spell));
        }

    }

    public IEnumerator CastCo(Spell spell) {
        casting = true;
        Vector2 spawn = (Vector2)player.getPlayerCastPoint();
        if(spell == Spell.BOLT) {

            //Instantiate bullet prefab and shot(castDirection);
            LinearBullet bullet = GameObject.Instantiate(player.boltPrefab, spawn, Quaternion.identity).GetComponent<LinearBullet>();
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), bullet.GetComponent<Collider2D>());
            yield return new WaitForSeconds(0.2f);

            SoundManager.Instance.playVoice(player.castClips[0]);
            bullet.setShotHeight(player.getMapHeight());
            int damage = SpellsManager.Instance.computeSpellDamage(spell);

            bullet.setDamage(damage);
            bullet.shot(worldPoint);
            castDone = true;

           

            UpdatePlayerSP(spell);
            player.spellCasted.Raise(spell);
            yield break;
        }
       
        if(spell == Spell.SHIELD) {

            float duration = SpellsManager.Instance.computeSpellDuration(spell);

            SoundManager.Instance.playVoice(player.castClips[1]);
            player.createShield(duration);
            UpdatePlayerSP(spell);
            player.spellCasted.Raise(spell);
           
            castDone = true;
            yield break;
        }

        if(spell == Spell.RANGE_ATTACK) {

            GameObject rayAttack = GameObject.Instantiate(player.rayAttkPrefab, spawn, Quaternion.identity);
            RangeAttk ray = rayAttack.GetComponent<RangeAttk>();
            yield return new WaitForSeconds(0.2f);
            int damage = SpellsManager.Instance.computeSpellDamage(spell);
            float duration = SpellsManager.Instance.computeSpellDuration(spell);

            ray.setDuration(duration);
            ray.setDamage(damage);
            SoundManager.Instance.playVoice(player.castClips[2]);
            ray.shot(castDirection);

            player.spellCasted.Raise(spell);
            
            castDone = true;
            UpdatePlayerSP(spell);
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

    public void UpdatePlayerSP(Spell spell) {

        int playerSP = player.playerSP.getRunTimeValue();

        int points = SpellsManager.Instance.computeSPConsumed(spell);

        playerSP -= points;
        player.playerSP.UpdateValue(playerSP);
    }
}
