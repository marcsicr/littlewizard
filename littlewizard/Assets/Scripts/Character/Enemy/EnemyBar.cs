using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBar : MonoBehaviour
{
    Enemy parent;
    float max;
    Color orange;
    SpriteRenderer spriteRenderer;
    //public void Update() {

    //    updateBar(parent.getHP());
    //}
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        transform.localScale = new Vector3(1f, 1f);
        parent = transform.GetComponentInParent<Enemy>();
        max = (float)parent.getHP();
        orange = new Color(255, 165, 0);

    }

    public void updateBar(int current) {
        //Debug.Log("Update bar called");
        float barLength = computeBarLength(current);
        transform.localScale = new Vector3(barLength, 1f);

        if (barLength < 0.65f && barLength > 0.3f) {
            spriteRenderer.color = orange;
        }
        if(barLength < 0.3f) {
            spriteRenderer.color = Color.red;
        }
    }

    /*Return float in range 0 <-> 1*/
    private float computeBarLength(int current) {

        if (current > 0) {
            float maxValue = (float)max;
            float currentValue = (float)current;
            return currentValue / maxValue;
        }
        return 0;
    }

}
