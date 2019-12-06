using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public Material laserMaterial;
    private LineRenderer line;


    private int LASER_HIT_LAYER;
    

    public float width;
    public float widthOffset;
    public float radius;


    bool exit = false;
    

    private GameObject lineContainer;
    public void Awake() {

         LASER_HIT_LAYER = 1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Wall");

        lineContainer = new GameObject();
        lineContainer.name = "LaserLine";
        lineContainer.transform.SetParent(this.transform.parent);
        lineContainer.transform.localPosition = Vector3.zero;

        line = lineContainer.AddComponent<LineRenderer>();
     
        
    }

    void Start(){

        line.transform.SetParent(this.transform);

        line.material = laserMaterial;
        line.sortingLayerName = "Player";
        line.sortingOrder = 1;
        line.startColor = Color.white;
        line.endColor = Color.white;
        line.startWidth = width;
        line.endWidth = width;

        line.useWorldSpace = true;
        line.alignment = LineAlignment.TransformZ;
        line.numCapVertices = 4;
    }

    /*public IEnumerator dissapear() {
        StartCoroutine(hideCo());
    }*/

    // Update is called once per frame
    void Update(){


        /*if (Input.GetKeyDown(KeyCode.K)) {

            shot(Vector2.right);
        }

        if (shooted) {

            timeout -= Time.deltaTime;
            if(timeout <= 0) {

                StartCoroutine(hideCo());
            }
        }*/
    }
    

    public IEnumerator dissapear() {

        exit = true;
        yield return null;
       
       
        float currentWidth = line.startWidth;
        while(currentWidth > 0) {

            line.startWidth = currentWidth;
            line.endWidth = currentWidth;

            currentWidth -= 0.01f;
            yield return null;
        }
 
        line.positionCount = 0;
        
    }

    public IEnumerator shotCo(Vector2 direction) {

        yield return new WaitForSeconds(0.2f);

      
        StartCoroutine(resizeEffectCo());
        StartCoroutine(moveCo(direction));
    }

    public void shot(Vector2 point) {

        exit = false;
        StartCoroutine(shotCo(point));

    }

    public IEnumerator moveCo(Vector2 direction) {

        line.positionCount = 2;

        float maxDegrees = 30;
        float z = 0;
        float degrees;
        Vector2 newDirection;

        while (!exit) {

            degrees = maxDegrees * Mathf.Cos(z);
            newDirection = Quaternion.Euler(0, 0, degrees) * direction;
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, line.startWidth, newDirection, radius, LASER_HIT_LAYER);

            if (hit.collider != null) {

                if (hit.collider.CompareTag("Player")) {

                  hit.collider.gameObject.GetComponent<Player>().OnGetKicked(1);
                }
            }
         
            //Debug.Log("beamVector" + newVector);
            line.SetPosition(0, transform.position);

            if(hit.point != Vector2.zero) {
                line.SetPosition(1, hit.point);
            } else {
                line.SetPosition(1, newDirection * radius);
            }
           


            z += 1.5f*Time.deltaTime;
           
           
            yield return null;
        }

    }


    public IEnumerator resizeEffectCo() {

        while (!exit) {

            float offsetSize = Random.Range(-widthOffset, widthOffset);

            line.startWidth = width + offsetSize;
            line.endWidth = width + offsetSize;
          
            yield return new WaitForSeconds(0.08f);
        }

        yield return null;
    }
}
