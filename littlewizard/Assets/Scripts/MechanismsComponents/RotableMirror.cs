using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class RotableMirror : MonoBehaviour{

    public enum Orientation {VerticalLeft,TopLeftCorner,HorizontalTop,TopRightCorner,VerticalRight,BottomRightCorner,HorizontalBottom,BottomLeftCorner};
  

    public Orientation currentOrientation;
    public GameObject MirrorVerticalPrefab;
    public GameObject MirrorHorizontalPrefab;
    public GameObject MirrorRotatedLeftPrefab;
    public GameObject MirrorRotadedRightPrefab;

    private GameObject current;


    void Start() {

        GameObject prefab = null;

        switch (currentOrientation) {
            case Orientation.VerticalLeft:
                prefab = MirrorVerticalPrefab;
                break;
            case Orientation.TopLeftCorner:
                prefab = MirrorRotadedRightPrefab;
                break;
            case Orientation.HorizontalTop:
                prefab = MirrorHorizontalPrefab;
                break;
            case Orientation.TopRightCorner:
                prefab = MirrorRotatedLeftPrefab;
                break;
            case Orientation.VerticalRight:
                prefab = MirrorVerticalPrefab;
                break;
            case Orientation.BottomRightCorner:
                prefab = MirrorRotadedRightPrefab;
                break;
            case Orientation.HorizontalBottom:
                prefab = MirrorHorizontalPrefab;
                break;
            case Orientation.BottomLeftCorner:
                prefab = MirrorRotatedLeftPrefab;
                break;
        }

        current = Instantiate(prefab, transform, false);
    }

    public void rotate() {

        //Vertical --> RotatedRight
        //RotatedRight --> Horizontal
        //Horizontal --> RotatedLeft

        GameObject prefab = null;

        switch (currentOrientation) {

            case Orientation.VerticalLeft:
                currentOrientation = Orientation.TopLeftCorner;
                prefab = MirrorRotadedRightPrefab;
               
                break;
            case Orientation.TopLeftCorner:
                currentOrientation = Orientation.HorizontalTop;
                prefab = MirrorHorizontalPrefab;
                break;

            case Orientation.HorizontalTop:
                currentOrientation = Orientation.TopRightCorner;
                prefab = MirrorRotatedLeftPrefab;
                break;

            case Orientation.TopRightCorner:
                currentOrientation = Orientation.VerticalRight;
                prefab = MirrorVerticalPrefab;
                break;

            case Orientation.VerticalRight:
                currentOrientation = Orientation.BottomRightCorner;
                prefab = MirrorRotadedRightPrefab;
                break;

            case Orientation.BottomRightCorner:
                currentOrientation = Orientation.HorizontalBottom;
                prefab = MirrorHorizontalPrefab;
                break;

            case Orientation.HorizontalBottom:
                currentOrientation = Orientation.BottomLeftCorner;
                prefab = MirrorRotatedLeftPrefab;
                break;

            case Orientation.BottomLeftCorner:
                currentOrientation = Orientation.VerticalLeft;
                prefab = MirrorVerticalPrefab;
                break;


        }

        Destroy(current);
        current = Instantiate(prefab, transform, false);
    }
}
