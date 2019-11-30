using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EArrow : MonoBehaviour
{
    private LineRenderer line;

    public Transform pointMiddle;
    public Transform target;


 

    


    public void Start() {

        line = GetComponent<LineRenderer>();
        line.useWorldSpace = true;


        
    }

    void Update() {

         //time += Time.deltaTime;
        Vector2 start = transform.position;
        Vector2 end = target.transform.position;
        Vector2 middle = Vector2.Lerp(start, end, 0.5f);

        Vector3 direction = (end - start).normalized;

        float angle = Vector2.SignedAngle(Vector2.right, direction);
        
       // Debug.Log("Angle" + angle);
        float distance = Vector3.Distance(start, end);

        Vector2 offset = Quaternion.Euler(0, 0, -90) * direction * distance * 0.25f * -Mathf.Cos(angle * Mathf.Deg2Rad);
        middle += offset;

        pointMiddle.position = middle;

        Vector2 firstQuarter = Vector2.Lerp(start, end, 0.25f) + offset;
        Vector2 thirdQuqrter = Vector2.Lerp(start, end, 0.75f) + offset;

        List<Vector2> points = getQuadraticBezierCurvePoints(start, middle, end, 25);
        //List<Vector2> points = getCubicBezierCurvePoints(start, firstQuarter,thirdQuqrter,end, 25);

        int i = 0;
        line.positionCount = points.Count;
        foreach (Vector2 point in points){

            line.SetPosition(i, point);

            i++;
        }
    }


    private List<Vector2> getQuadraticBezierCurvePoints(Vector2 p0, Vector2 p1, Vector2 p2,int samples) {

        
        List<Vector2> list = new List<Vector2>();

        float points = 2 + samples;

        float segment = 1 / points;

        for(float t = 0; t<=1; t+= segment) {

            list.Add(quadraticBezierPoint(t, p0, p1, p2));
        }

        return list;
    }

    Vector2 quadraticBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2) {

        //B(t) = (1-t)^2 * P0 + 2(1-t)*t*P1 + t^2*P2
        return  (1 - t) * (1 - t) * p0 + 2 * (1 - t) * t * p1 + t * t * p2;
        
    }

    Vector2 cubicBezierPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2,Vector2 p3) {

        //B(t) = (1-t)^3 * P0 + 3(1-t)^2*t*P1  + 3(1-t)*t^2 * P2 + t^3*P3

        float u = 1 - t;
        float uu = u * u;
        float tt = t * t;

        return u * uu * p0 + 3 * uu * t * p1 + 3 * u * tt * p2 + tt* t * p3;

    }


    private List<Vector2> getCubicBezierCurvePoints(Vector2 p0, Vector2 p1,Vector2 p2, Vector2 p3, int samples) {


        List<Vector2> list = new List<Vector2>();

        float points = 2 + samples;

        float segment = 1 / points;

        for (float t = 0; t <= 1; t += segment) {

            list.Add(cubicBezierPoint(t, p0, p1, p2,p3));
        }

        return list;
    }

}
