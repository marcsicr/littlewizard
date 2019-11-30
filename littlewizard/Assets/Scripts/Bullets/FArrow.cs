using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FArrow: Bullet{

    private Vector3 targetPoint;


    private void Start() {

        targetPoint = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().getCollisionCenterPoint();
       // myRigidBody = GetComponent<Rigidbody2D>();

      //  myAnimator = GetComponent<Animator>();
   
        Vector3 startDir = (targetPoint - transform.position).normalized;
        myAnimator.SetFloat("X", startDir.x);
        myAnimator.SetFloat("Y", startDir.y);

    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.E)) {

            shot(targetPoint);
          
        }
    }


    private void arrowShot() {

        targetPoint = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().getCollisionCenterPoint();
        StartCoroutine(ShotCo());
    }

    private IEnumerator ShotCo() {

        Vector2 start = transform.position;
        Vector2 end = targetPoint;

        Vector2 middle = Vector2.Lerp(start, end, 0.5f);
        Vector3 direction = (end - start).normalized;

        float angle = Vector2.SignedAngle(Vector2.right, direction);


        float distance = Vector3.Distance(start, end);

        Vector2 offset = Quaternion.Euler(0, 0, -90) * direction * distance * 0.5f * -Mathf.Cos(angle * Mathf.Deg2Rad);
        middle += offset;

      

        for (float t = 0; t<=1; t += 0.01f) {

            myRigidBody.position = quadraticBezierPoint(t, start, middle, end);

            Vector2 dir = quadraticBezierPointDerivative(t, start, middle, end);

            myAnimator.SetFloat("X", dir.x);
            myAnimator.SetFloat("Y", dir.y);

            //Debug.Log("Direction+" + dir);
            yield return new WaitForEndOfFrame();
        }


     
    }


    private Vector2 quadraticBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2) {

        //B(t) = (1-t)^2 * P0 + 2(1-t)*t*P1 + t^2*P2
        return (1 - t) * (1 - t) * p0 + 2 * (1 - t) * t * p1 + t * t * p2;

    }

    private Vector2 quadraticBezierPointDerivative(float t, Vector2 p0, Vector2 p1, Vector2 p2) {

        //B'(t) = 2*(1-t) (P1-P0) + 2*t (P2-P1)
        float u = 1 - t;
        Vector2 res = 2 * u * (p1 - p0) + 2 * t * (p2 - p1);

        res.Normalize();

        return res;

    }

    public override void shot(Vector2 point) {

        arrowShot();
    }

    /*public override void shotOnPoint(Vector3 point) {
        arrowShot();
    }*/

    public override void onCollision(Vector2 collisionPoint) {

        Destroy(gameObject);
    }
}
