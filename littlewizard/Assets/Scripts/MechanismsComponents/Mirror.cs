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
        if (other.CompareTag("Bullet")) {

            LinearBullet bullet = other.GetComponent<LinearBullet>();

            if (bullet != null) {

                Vector2 direction = bullet.getDirection();

                Vector2 newDirection = reflect(direction);
                bullet.setDirection(newDirection);

                

                //Debug.Log("New angle" + Vector2.SignedAngle(Vector2.right, newDirection));
            }

        }
        
    }

    public Vector2 reflect(Vector2 direction) {

        Vector2 res;
        res = Vector2.Reflect(direction, mirrorNormal);
        res = roundDirection(res);

        return res;
    }

    private Vector2 roundDirection(Vector2 direction) {

        direction.x = Mathf.Round(direction.x * 10f) / 10f;
        direction.y = Mathf.Round(direction.y * 10f) / 10f;

        return direction;
    }
}
