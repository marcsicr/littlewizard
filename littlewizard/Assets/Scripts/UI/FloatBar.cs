using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FloatBar : MonoBehaviour
{
    public ObservableFloat var;
    private FloatObserver observer;


    private void Awake() {
        observer = new FloatObserver(var, updateBar);
        transform.localScale = new Vector3(1f, 1f);
    }

    public void updateBar(float newValue) {
        float barLength = computeBarLength(var.getInitialValue(),newValue);
        transform.localScale = new Vector3(barLength, 1f);
    }

    /*Return float in range 0 <-> 1*/
    private float computeBarLength(float max, float current) {
        
        if (current > 0) {
            float maxValue = (float)max;
            float currentValue = (float)current;
            return currentValue / maxValue;
        }
        return 0;
    }


    private void OnDisable() {
        observer.stopObserving(); //Important!
    }
}
