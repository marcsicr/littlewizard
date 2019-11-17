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

            direction = roundDirection(direction);
            Vector2 newDirection = Vector2.Reflect(direction, mirrorNormal);

            newDirection = roundDirection(newDirection);
            bullet.setDirection(newDirection);
           
            Debug.Log("New angle" + Vector2.SignedAngle(Vector2.right,newDirection));
        }
        
    }

    private Vector2 roundDirection(Vector2 direction) {

        direction.x = Mathf.Round(direction.x * 10f) / 10f;
        direction.y = Mathf.Round(direction.y * 10f) / 10f;

        return direction;
    }
}
