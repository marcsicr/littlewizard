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



        float dist = 0.3f;
        Vector2 v1 = 2 * start - 4 * middle + 2 * end;
        Vector2 v2 = -2 * start + 2 * middle;

        for (float t = 0; t <= 1; t += dist / (t * v1 + v2).magnitude) {

            myRigidBody.position = quadraticBezierPoint(t, start, middle, end);

            transform.Rotate(Vector3.forward * 5);
            //Vector2 dir = quadraticBezierPointDerivative(t, start, middle, end);
            //Debug.Log("Direction+" + dir);
            yield return new WaitForEndOfFrame();
        }

        GetComponent<Collider2D>().enabled = false;
        SoundManager.Instance.playEffect(bulletHitClip);
        StartCoroutine(dissapearCo(1));

        



    }


    IEnumerator dissapearCo(float duration) {

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Color start = renderer.color;
        Color end = start;
        end.a = 0;

        for (float t = 0f; t < duration; t += Time.deltaTime) {
            float normalizedTime = t / duration;

            renderer.color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }

        Destroy(gameObject);
    }
}
