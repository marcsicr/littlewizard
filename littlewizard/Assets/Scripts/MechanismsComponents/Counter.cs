using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Counter : MonoBehaviour
{
    TextMeshPro text;
    void Start(){

        text = transform.GetComponentInChildren<TextMeshPro>();
    }


   public void setText(string str) {

        text.text = str;
    }
}
