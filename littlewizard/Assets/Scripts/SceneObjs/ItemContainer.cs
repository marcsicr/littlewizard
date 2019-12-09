﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemContainer : MonoBehaviour{

    public static string TAG = "ItemContainer";
    protected Animator myAnimator;
    public Vector2 throwDirection = Vector2.up;
    public Item throwItem;

    private void Awake() {
        myAnimator = GetComponent<Animator>();
    }


    public abstract void open();


    public void throwLoot() {

        if(throwItem != null)
         StartCoroutine(throwLootCo());
       
    }

    protected IEnumerator throwLootCo() {
        
        Vector2 start = transform.position;
        Vector2 end = start + Random.insideUnitCircle.normalized * 2;

        Vector2 middle = Bezier.computeElipticalP1(start, end);
        
       Item itemInstance = GameObject.Instantiate(throwItem, transform.position, Quaternion.identity).GetComponent<Item>();
       itemInstance.setCollectable(false);

        for (float t = 0; t <= 1; t +=0.05f) {

            itemInstance.transform.position = Bezier.quadraticBezierPoint(t, start, middle, end);
            yield return new WaitForEndOfFrame();
        }

        itemInstance.setCollectable(true);
    }
}