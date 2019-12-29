using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PortalTransfer : MonoBehaviour
{
    public PortalTransfer outPortal;

    public Signal transitionEnter;
    public Signal transitionOut;

    private BoundsManager boundsMan;
        
    GameObject player;
    CameraPlayer cam;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraPlayer>();

        //Potal is expected to be in /Map/portals/portalX Bounds is expected to be in /Map/Bounds
        boundsMan = outPortal.gameObject.transform.parent.parent.Find("Bounds").GetComponent<BoundsManager>();
        if (boundsMan == null)
            Debug.Log("PortalTransfer: nextMapBounds not found");


        //Debug.Log(boundsComponent.transform.parent.name + "BOUNDS" + "Top left " + boundsComponent.topLeft + "Top right " + boundsComponent.bottomRight);
    }

    public void OnTriggerEnter2D(Collider2D other) {
        
        if(other.tag == "Player") {

            transitionEnter.Raise();
            StartCoroutine(waitEneter());
        }
    }


    private IEnumerator waitEneter() {


        RectBoundaries b = boundsMan.getBoundaries();
        yield return new WaitForSeconds(0.2f);
        cam.updateBoundaries(b.topLeft, b.bottomRight,boundsMan.CameraSize);
        cam.transform.position = outPortal.transform.position;
        player.transform.position = outPortal.transform.position;
        
        transitionOut.Raise();

        SoundManager.Instance.changeSong(boundsMan.zoneSong);
    }
}
