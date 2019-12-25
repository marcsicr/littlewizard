﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected bool collectable = true;
    private void OnTriggerEnter2D(Collider2D other) {

        if(other.CompareTag(Player.TAG) && collectable) {
            Player player = other.GetComponent<Player>();
            onItemCollect(player);
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(collectEffectCo());
           
        }
        
    }

    private IEnumerator collectEffectCo() {

        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        float duration = 1;
        


        Color end = new Color32(1, 1, 1, 0);

        for (float t = 0f; t < duration; t += Time.deltaTime) {
            float normalizedTime = t / duration;

            spr.color = Color.Lerp(spr.color, end, normalizedTime);

            transform.position = new Vector3(transform.position.x, transform.position.y + 2 * Time.deltaTime, 0);
            yield return null;
        }
        spr.color = end;
        Destroy(gameObject);

    }


    public void setCollectable(bool isCollectable) {
        collectable = isCollectable;
    }

    public abstract void onItemCollect(Player player);
}
