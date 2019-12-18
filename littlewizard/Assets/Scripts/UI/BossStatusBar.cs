﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossStatusBar : MonoBehaviour
{
  
    Image faceImg;
    public void Awake() {
        faceImg = transform.Find("PictureMask/CharacterImg").GetComponent<Image>();
    }

    

    public void setFace(Sprite image) {
        faceImg.sprite = image;
    }
}
