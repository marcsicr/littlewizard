using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    public float smoothing;
    public Vector2 topLeft;
    public Vector2 bottomRight;
    public BoundsManager initialBounds;
    private bool shaking = false;

    //Camera boundaries to make it Camera size independent
    private Vector3 wtl; // World point at (topLeft.x + CameraWidth/2 , topLeft.y - CemaraHeight/2)
    private Vector3 wbr; // World point at (bottomRight.x - CameraWidth/2 , topLeft.y + CemaraHeight/2)  
   

    // Start is called before the first frame update
    void Start()
    {
        RectBoundaries bounds = initialBounds.getBoundaries();
        computeCamBundaries(bounds.topLeft, bounds.bottomRight);
        //computeCamBundaries(topLeft, bottomRight);
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        cam = gameObject.GetComponent<Camera>();
        //Debug.Log("Pixel width :" + cam.pixelWidth + " Pixel height : " + cam.pixelHeight);
       
    }

   
    void LateUpdate()
    {

        if (!shaking) {

            Vector3 targetPosition = target.position + new Vector3(0, 0, -1);
            Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothing);
            transform.position = new Vector3(Mathf.Clamp(smoothPosition.x, wtl.x, wbr.x), Mathf.Clamp(smoothPosition.y, wbr.y, wtl.y), smoothPosition.z);
        }
       
    }

    public void updateBoundaries(Vector2 topLeft, Vector2 bottomRight,float size) {

        this.topLeft = topLeft;
        this.bottomRight = bottomRight;
        cam.orthographicSize = size;// Mathf.Clamp(size, 5, 12);
        computeCamBundaries(topLeft, bottomRight);

    }

    /**
     * Compute camera boundaries to make it Camera size independent
     */
    private void computeCamBundaries( Vector2 topLeft, Vector2 bottomRight) {

        Vector3 topLeftToCam = cam.WorldToScreenPoint(topLeft);
        topLeftToCam.x += cam.pixelWidth / 2;
        topLeftToCam.y -= cam.pixelHeight / 2;

        Vector3 bottomRightToCam = cam.WorldToScreenPoint(bottomRight);
        bottomRightToCam.x -= cam.pixelWidth / 2;
        bottomRightToCam.y += cam.pixelHeight / 2;

        wtl = cam.ScreenToWorldPoint(topLeftToCam);
        wbr = cam.ScreenToWorldPoint(bottomRightToCam);
    }

  
    public IEnumerator shakeCo(float timeout,float shakeMagnitude) {
        shaking = true;
        yield return null;
        Vector3 origin = transform.localPosition;
        while(timeout > 0) {
            transform.localPosition = origin + Random.insideUnitSphere * shakeMagnitude;
            timeout -= Time.deltaTime;
            yield return null;
        }

        shaking = false;
        
    }
}
