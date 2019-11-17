using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{

    public Vector2 mirrorNormal;
    
    private void Awake() {
        mirrorNormal.Normalize();
    }
    private void OnTriggerEnter2D(Collider2D other) {

        //Vector2 mirrorNormal = new Vector2(1f, -1f);

        if (other.CompareTag("Bullet")) {

            Bullet bullet = other.GetComponent<Bullet>();

            Vector2 direction = bullet.getDirection();

            direction.x = Mathf.Round(direction.x);
            direction.y = Mathf.Round(direction.y);
            Vector2 newDirection = Vector2.Reflect(direction, mirrorNormal);

            bullet.setDirection(newDirection);
           
            Debug.Log("New angle" + Vector2.SignedAngle(Vector2.right,newDirection));
        }
        
    }
}
