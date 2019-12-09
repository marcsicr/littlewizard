using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellLevelBar : MonoBehaviour{

    public Sprite lvl0;
    public Sprite lvl1;
    public Sprite lvl2;
    public Sprite lvl3;
    public Sprite lvl4;
    public Sprite lvl5;

    public void setLevel(int i) {

        Sprite spr = lvl0;

        switch (i) {
            case 1:
                spr = lvl1;
                break;
            case 2:
                spr = lvl2;
                break;
            case 3:
                spr = lvl3;
                break;
            case 4:
                spr = lvl4;
                break;
            case 5:
                spr = lvl5;
                break;
            
        }

        GetComponent<Image>().sprite = spr;
    }

}
