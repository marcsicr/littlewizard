using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier
{
    public static Vector2 quadraticBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2) {

        //B(t) = (1-t)^2 * P0 + 2(1-t)*t*P1 + t^2*P2
        return (1 - t) * (1 - t) * p0 + 2 * (1 - t) * t * p1 + t * t * p2;

    }

    public static Vector2 quadraticBezierPointDerivative(float t, Vector2 p0, Vector2 p1, Vector2 p2) {

        //B'(t) = 2*(1-t) (P1-P0) + 2*t (P2-P1)
        float u = 1 - t;
        Vector2 res = 2 * u * (p1 - p0) + 2 * t * (p2 - p1);

        res.Normalize();

        return res;

    }

    public static Vector2 computeElipticalP1(Vector2 start, Vector2 end) {

        Vector2 middle = Vector2.Lerp(start, end, 0.5f);
        Vector3 direction = (end - start).normalized;

        float angle = Vector2.SignedAngle(Vector2.right, direction);


        float distance = Vector3.Distance(start, end);

        Vector2 offset = Quaternion.Euler(0, 0, -90) * direction * distance * 0.5f * -Mathf.Cos(angle * Mathf.Deg2Rad);
        middle += offset;

        return middle;
    }
}
