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


    public void throwLoot() {

        SoundManager.Instance.playEffect(openClip);
        StartCoroutine(throwLootCo(throwItems));
        
    }

    protected IEnumerator throwLootCo(Item[] throwItems) {
        
        foreach (Item throwItem in throwItems) {

            Vector2 start = transform.position;


            Vector2 end = start + Random.insideUnitCircle.normalized * 2;

            RaycastHit2D hit = Physics2D.CircleCast(start, 0.4f, end - start);
            //Random.InitState(System.DateTime.Now.Millisecond);

            while (hit.collider.tag == "Untagged" || hit.collider.tag == "Item"){
                end = start + Random.insideUnitCircle.normalized * 2;
                hit = Physics2D.CircleCast(start, 0.4f, end - start);
                yield return null;
            }

            // = Physics2D.Raycast(start, end - start);
            /* if (hit.collider.tag != "ItemContainer" && hit.collider.tag != "Item") {
                 Debug.Log("Hitted something:" + hit.collider.tag);
             }*/

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
}
