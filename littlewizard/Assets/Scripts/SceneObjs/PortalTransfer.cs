using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PortalTransfer : MonoBehaviour
{
    public PortalTransfer outPortal;
    [HideInInspector]
    public Zone zone;
    public Signal transitionEnter;
    public Signal transitionOut;

    private BoundsManager outBoundsMan;
        
    Player player;
    CameraPlayer cam;
    
    // Start is called before the first frame update
    void Start()
    {
        player = Player.GetPlayer();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraPlayer>();
        zone = LevelManager.Instance.getZone(transform.position);

        //Potal is expected to be in /Map/portals/portalX Bounds is expected to be in /Map/Bounds
        outBoundsMan = outPortal.gameObject.transform.parent.parent.Find("Bounds").GetComponent<BoundsManager>();
        if (outBoundsMan == null)
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


        RectBoundaries b = outBoundsMan.getBoundaries();
        yield return new WaitForSeconds(0.2f);
        cam.updateBoundaries(b.topLeft, b.bottomRight,outBoundsMan.zone.cameraSize);
        cam.transform.position = outPortal.transform.position;
        outPortal.zone.changeActive(true);
        zone.changeActive(false);
        player.transform.position = outPortal.transform.position;
        
        transitionOut.Raise();

        SoundManager.Instance.changeSong(outBoundsMan.zone.zoneSong);
    }
}
