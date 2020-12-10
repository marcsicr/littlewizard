using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePointer : MonoBehaviour{

   
    private LineRenderer line;
    private LineRenderer reflectedLine;
    public float spawnDelay;
    public float maxDistance;
    int layerMask;

   

    void Start(){

        layerMask = ~(1 << LayerMask.NameToLayer("Background") | 
                      1 << LayerMask.NameToLayer("Player") | 
                      1 << LayerMask.NameToLayer("Bullet") |
                      1 << LayerMask.NameToLayer("Item"));

        line = GetComponent<LineRenderer>();
        reflectedLine = transform.Find("LineReflect").GetComponent<LineRenderer>();

        

  
        Color halfAlpha = Color.white;
        halfAlpha.a = 0.5f;
        reflectedLine.endColor = halfAlpha;
        reflectedLine.useWorldSpace = true;
        line.useWorldSpace = true;
        line.enabled = false;
        reflectedLine.enabled = false;
    }


    // Update is called once per frame
    void LateUpdate()
    {
        
       /* if (Input.GetMouseButton(0) && castMgr.getSelectedSpell() == Spell.BOLT ) {

            timeDown += Time.deltaTime;

            if(timeDown >= spawnDelay) {
                 showLine();
            }
  
        } else {

            timeDown = 0;
            hideLine();
        }*/
    }

    //private void showLine() {

    //    line.enabled = true;
    //    Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    Vector2 direction = position - transform.position;

    //    direction.Normalize();
    //    // Debug.Log("Direction:" + direction);


    //    RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.2f, direction, maxDistance, layerMask);
    //    line.positionCount = 2;
    //    line.SetPosition(0, transform.position);
    //    if(hit.collider == null) {

    //        Vector2 pos = (Vector2)transform.position + direction * maxDistance;
    //        line.SetPosition(1, pos);
    //        reflectedLine.enabled = false;
    //        return;
    //    } 

       
    //    if (hit.collider.CompareTag("Mirror")) {

    //            //Debug.Log("Line start:" + transform.position + "Line direction:" + direction);

    //            Mirror m = hit.collider.gameObject.GetComponent<Mirror>();
    //            line.SetPosition(1, hit.point);
    //            reflectedLine.positionCount = 2;
    //            reflectedLine.SetPosition(0, hit.point);
    //            reflectedLine.SetPosition(1, hit.point + m.reflect(direction) * 3);
    //            reflectedLine.enabled = true;

    //    } else {

    //        line.SetPosition(1, hit.point);
    //        reflectedLine.enabled = false;
    //    }
        
        

      
    //}


    public void show(Vector3 startPoint, Vector2 direction) {

        line.enabled = true;
       // Vector2 direction = endPoint - startPoint;

        direction.Normalize();
        // Debug.Log("Direction:" + direction);


        RaycastHit2D hit = Physics2D.CircleCast(startPoint, 0.2f, direction, maxDistance, layerMask);
        line.positionCount = 2;
        line.SetPosition(0, startPoint);
        if (hit.collider == null) {

            Vector2 pos = (Vector2)startPoint + direction * maxDistance;
            line.SetPosition(1, pos);
            reflectedLine.enabled = false;
            return;
        }


        if (hit.collider.CompareTag("Mirror")) {

            //Debug.Log("Line start:" + transform.position + "Line direction:" + direction);

            Mirror m = hit.collider.gameObject.GetComponent<Mirror>();
            line.SetPosition(1, hit.point);
            reflectedLine.positionCount = 2;
            reflectedLine.SetPosition(0, hit.point);
            reflectedLine.SetPosition(1, hit.point + m.reflect(direction) * 3);
            reflectedLine.enabled = true;

        } else {

            line.SetPosition(1, hit.point);
            reflectedLine.enabled = false;
        }




    }


   public void hideLine() {
       
        line.enabled = false;
        reflectedLine.enabled = false;
    }
}
