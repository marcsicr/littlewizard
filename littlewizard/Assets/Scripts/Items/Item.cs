using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected bool collectable = true;
    private void OnTriggerEnter2D(Collider2D other) {

        if(other.CompareTag(Player.TAG) && collectable) {
            Player player = other.GetComponent<Player>();
            onItemCollect(player);
            gameObject.SetActive(false);
            //Destroy(gameObject,1);
        }
        
    }

    public void setCollectable(bool isCollectable) {
        collectable = isCollectable;
    }

    public abstract void onItemCollect(Player player);
}
