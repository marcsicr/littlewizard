using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public SONG zoneSong;
    public float cameraSize;

    private ZoneGrid grid;
    private BoundsManager boundsMan;
    private Transform content;


    public void Awake() {
        grid = transform.Find("Content/Grid").GetComponent<ZoneGrid>();
        boundsMan = transform.Find("Bounds").GetComponent<BoundsManager>();
        
        foreach (Transform t in transform) {

            if(t.name == "Content") {
                content = t;
                break;
            }
        }
        //content = transform.GetComponentInChildren<Transform>(true);
       
        
    }

    public void changeActive(bool isActive) {
        
        content.gameObject.SetActive(isActive);
    }

    public bool isInsideZone(Vector2 position) {

        bool res = false;
        RectBoundaries bounds = boundsMan.getBoundaries();

        if (bounds.topLeft.x <= position.x && 
            bounds.bottomRight.x >= position.x && 
            bounds.topLeft.y >= position.y && 
            bounds.bottomRight.y <= position.y)
            res = true;

        return res;
    }

    public RectBoundaries getZoneBounds() {

        return boundsMan.getBoundaries();
    }

    public ZoneGrid getZoneGrid() {
        return grid;
    }
}
