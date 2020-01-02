using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemContainer : MonoBehaviour{

    public AudioClip openClip;
    public static string TAG = "ItemContainer";
    protected Animator myAnimator;
    public Item[] throwItems;

 

    private void Awake() {
        myAnimator = GetComponent<Animator>();
    }


    public abstract void open();
    public abstract Vector3 getThrowPoint();

    public void throwLoot() {

        SoundManager.Instance.playEffect(openClip);
        StartCoroutine(throwLootCo(throwItems));    
    }

    protected IEnumerator throwLootCo(Item[] throwItems) {
        
        foreach (Item throwItem in throwItems) {

            Vector2 end = getThrowPoint() + Random.insideUnitSphere;

            StartCoroutine(throwItemCo(throwItem,end));

            yield return new WaitForSeconds(0.1f);
        }
       
    }

    private IEnumerator throwItemCo(Item throwItem,Vector2 throwPosition) {

        Vector2 start = transform.position;


        Vector2 end = throwPosition;
        Vector2 middle = Bezier.computeElipticalP1(start, end);

        Item itemInstance = GameObject.Instantiate(throwItem, transform.position, Quaternion.identity).GetComponent<Item>();
        itemInstance.setCollectable(false);

        for (float t = 0; t <= 1; t += 0.05f) {

            itemInstance.transform.position = Bezier.quadraticBezierPoint(t, start, middle, end);
            yield return new WaitForEndOfFrame();
        }

        itemInstance.setCollectable(true);
    }
}
