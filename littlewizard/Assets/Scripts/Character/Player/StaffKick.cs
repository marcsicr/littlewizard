using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffKick : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D other) {
        
        if(other.gameObject.tag == Enemy.ENEMY_TAG) {

            Debug.Log("Collided with enemy");
        }
    }
}
