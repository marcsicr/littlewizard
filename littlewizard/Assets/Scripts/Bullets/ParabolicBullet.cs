using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParabolicBullet : Bullet {

    public bool updateDirection = false;
    public bool arrowType = false;


    public override void shot(Vector2 worldPoint) {

        Vector2 direction = ((Vector2)transform.position - worldPoint).normalized;
        this.gameObject.SetActive(true);

        if (arrowType) {
            myAnimator.SetFloat("X", direction.x);
            myAnimator.SetFloat("Y", direction.y);
        }

        StartCoroutine(ParabolicShotCo(worldPoint));
    }

    private IEnumerator ParabolicShotCo(Vector2 worldPoint) {

        Vector2 start = transform.position;
        Vector2 end = worldPoint;

        Vector2 parabolicMiddle = Bezier.computeElipticalP1(start, end);


        //https://gamedev.stackexchange.com/questions/27056/how-to-achieve-uniform-speed-of-movement-on-a-bezier-curve
        float dist = speed; // Distance traveled each step
        Vector2 v1 = 2 * start - 4 * parabolicMiddle + 2 * end;
        Vector2 v2 = -2 * start + 2 * parabolicMiddle;

        for (float t = 0; t <= 1; t += dist / (t * v1 + v2).magnitude) {

            myRigidBody.position = Bezier.quadraticBezierPoint(t, start, parabolicMiddle, end);

            if (updateDirection) {

                shotDirectionUpdate(t,start,parabolicMiddle,end);
            }

            //Debug.Log("Direction+" + dir);
            yield return new WaitForEndOfFrame();
        }

        SoundManager.Instance.playEffect(bulletHitClip);
        GetComponent<Collider2D>().enabled = false;

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

    public virtual void shotDirectionUpdate(float t, Vector2 start, Vector2 parabolicMiddle, Vector2 end) {

        Vector2 dir = Bezier.quadraticBezierPointDerivative(t, start, parabolicMiddle, end);
        myAnimator.SetFloat("X", dir.x);
        myAnimator.SetFloat("Y", dir.y);
    }
}
