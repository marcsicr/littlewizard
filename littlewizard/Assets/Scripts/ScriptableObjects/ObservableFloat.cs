using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObservableFloat : ScriptableObject, ISerializationCallbackReceiver {
    
    public float initialValue;
    private List<FloatObserver> observers = new List<FloatObserver>();

    [System.NonSerialized]
    private float runtimeValue;

    public void OnAfterDeserialize() {
        runtimeValue = initialValue;
    }

   
    public void OnBeforeSerialize() {
       
    }

    public void UpdateValue(float i) {
        this.runtimeValue = i;
        NotifyObservers();
    }

    public float getRunTimeValue() {
        return this.runtimeValue;
    }
    public float getInitialValue() {
        return this.initialValue;
    }


    private void NotifyObservers() {
        foreach (FloatObserver o in observers) {

            o.OnVarChanged(runtimeValue);
        }
    }

    public void AddObserver(FloatObserver observer) {

        observers.Add(observer);
    }

    public void RemoveObserver(FloatObserver observer) {
        observers.Remove(observer);

    }

    public void reset() {
        this.runtimeValue = initialValue;
        //NotifyObservers();
    }
}

 
