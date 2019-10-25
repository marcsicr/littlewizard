using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PortalTransfer : MonoBehaviour
{
    public PortalTransfer portal;
    public Boundaries mapBoundaries;
    public Signal transitionEnter;
    public Signal transitionOut;

  
        
    GameObject player;
    CameraPlayer cam;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraPlayer>();
        
       
    }

    public void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "Player") {

            transitionEnter.Raise();
            StartCoroutine(waitEneter());
            transitionOut.Raise();

        }
    }

    public Vector2 getTopLeftBoundaries() {
        return mapBoundaries.topLeft;
    }

    public Vector2 getBottomRightBoundaries() {

        return mapBoundaries.bottomRight;
    }
    private IEnumerator waitEneter() {
       yield return new WaitForSeconds(0.2f);
        cam.updateBoundaries(portal.getTopLeftBoundaries(), portal.getBottomRightBoundaries());
        player.transform.position = portal.transform.position;
        cam.transform.position = portal.transform.position;
    }
}
