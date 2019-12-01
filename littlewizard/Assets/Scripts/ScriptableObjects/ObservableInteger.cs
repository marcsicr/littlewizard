using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObservableInteger : ScriptableObject, ISerializationCallbackReceiver {
    
    public int initialValue;
    private List<IntegerObserver> observers = new List<IntegerObserver>();

    [System.NonSerialized]
    private int runtimeValue;

    public void OnAfterDeserialize() {
        runtimeValue = initialValue;
    }

   
    public void OnBeforeSerialize() {
       
    }

    public void UpdateValue(int i) {
        this.runtimeValue = i;
        NotifyObservers();
    }

    public int getRunTimeValue() {
        return this.runtimeValue;
    }
    public int getInitialValue() {
        return this.initialValue;
    }


    private void NotifyObservers() {
        foreach (IntegerObserver o in observers) {

            o.OnVarChanged(runtimeValue);
        }
    }

    public void AddObserver(IntegerObserver observer) {

        observers.Add(observer);
    }

    public void RemoveObserver(IntegerObserver observer) {
        observers.Remove(observer);

    }

    public void reset() {
        this.runtimeValue = initialValue;
        //NotifyObservers();
    }
}

 
