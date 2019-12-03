using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : ElipticalBullet
{
    public override void onCollision(Vector2 collisionPoint) {

        Destroy(gameObject);
        
    }

    public override void shot(Vector2 worldPoint) {


        StartCoroutine(RockTrhowCo(worldPoint));

    }

    private IEnumerator RockTrhowCo(Vector2 point) {

        Vector2 start = transform.position;
        Vector2 end = point;

        Vector2 middle = Vector2.Lerp(start, end, 0.5f);
        Vector3 direction = (end - start).normalized;

        float angle = Vector2.SignedAngle(Vector2.right, direction);


        float distance = Vector3.Distance(start, end);

        Vector2 offset = Quaternion.Euler(0, 0, -90) * direction * distance * 0.5f * -Mathf.Cos(angle * Mathf.Deg2Rad);
        middle += offset;



        for (float t = 0; t <= 1; t += 0.01f + speed * Time.deltaTime) {

            myRigidBody.position = quadraticBezierPoint(t, start, middle, end);

            transform.Rotate(Vector3.forward * 5);
            //Vector2 dir = quadraticBezierPointDerivative(t, start, middle, end);
            //Debug.Log("Direction+" + dir);
            yield return new WaitForEndOfFrame();
        }

        Destroy(gameObject, 1);

    }


    private void Update() {

        if (Input.GetKeyDown(KeyCode.Space)) {

            Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

            shot(p.getCollisionCenterPoint());
        }
    }
}
