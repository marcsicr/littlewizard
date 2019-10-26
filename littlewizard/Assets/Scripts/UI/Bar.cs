using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bar : IntObserver
{

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(1f, 1f);
    }


    public void updateBar(ObservableInt var) {
        //Debug.Log("Update bar called");
        float barLength = computeBarLength(var.getInitialValue(),var.getRunTimeValue());
        transform.localScale = new Vector3(barLength, 1f);
    }

    /*Return float in range 0 <-> 1*/
    private float computeBarLength(int max, int current) {
        
        if (current > 0) {
            float maxValue = (float)max;
            float currentValue = (float)current;
            return currentValue / maxValue;
        }
        return 0;
    }


}
