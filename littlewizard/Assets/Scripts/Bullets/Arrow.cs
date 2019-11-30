using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : ElipticalBullet{


    public override void shot(Vector2 point) {

        Vector2 direction = ((Vector2)transform.position - point).normalized;
        this.gameObject.SetActive(true);

        //startMoving(direction);
        myAnimator.SetFloat("X", direction.x);
        myAnimator.SetFloat("Y", direction.y);
        Destroy(gameObject, lifetime);


        StartCoroutine(ArrowShotCo(point));
        
    }


    private IEnumerator ArrowShotCo(Vector2 point) {

        Vector2 start = transform.position;
        Vector2 end = point;

        Vector2 middle = Vector2.Lerp(start, end, 0.5f);
        Vector3 direction = (end - start).normalized;

        float angle = Vector2.SignedAngle(Vector2.right, direction);


        float distance = Vector3.Distance(start, end);

        Vector2 offset = Quaternion.Euler(0, 0, -90) * direction * distance * 0.5f * -Mathf.Cos(angle * Mathf.Deg2Rad);
        middle += offset;



        for (float t = 0; t <= 1; t += 0.01f  + speed *Time.deltaTime) {

            myRigidBody.position = quadraticBezierPoint(t, start, middle, end);

            Vector2 dir = quadraticBezierPointDerivative(t, start, middle, end);

            myAnimator.SetFloat("X", dir.x);
            myAnimator.SetFloat("Y", dir.y);

            //Debug.Log("Direction+" + dir);
            yield return new WaitForEndOfFrame();
        }



    }

    public override void onCollision(Vector2 collisionPoint) {
        Destroy(gameObject);
    }
}
