using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IntBar : MonoBehaviour
{
    public ObservableInteger var;
    private IntegerObserver observer;


    private void Awake() {
        observer = new IntegerObserver(var, updateBar);
        transform.localScale = new Vector3(1f, 1f);
    }

    void Start() {
        updateBar(var.getRunTimeValue());
    }

    public void updateBar(int newValue) {
        float barLength = computeBarLength(var.getInitialValue(),newValue);
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


    private void OnDisable() {
        observer.stopObserving(); //Important!
    }
}
