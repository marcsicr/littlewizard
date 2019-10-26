using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {

        if(other.tag  == "Player") {
            Player player = other.GetComponent<Player>();
            onItemCollect(player);
            Destroy(gameObject);
        }
        
    }

    public abstract void onItemCollect(Player player);
}
