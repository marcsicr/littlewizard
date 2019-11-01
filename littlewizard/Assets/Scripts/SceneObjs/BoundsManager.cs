using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RectBoundaries {

    public Vector2 topLeft;
    public Vector2 bottomRight;
}
public class BoundsManager : MonoBehaviour
{

    RectBoundaries mapBoundaries;
  

    public float CameraSize;

    public void Awake() {

        mapBoundaries.topLeft = transform.Find("TopLeft").position;
        mapBoundaries.bottomRight = transform.Find("BottomRight").position;

    }

    public RectBoundaries getBoundaries() {
        return mapBoundaries;
    }

}
