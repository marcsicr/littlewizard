using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObservableInt : ScriptableObject, ISerializationCallbackReceiver {
    public int initialValue;
    private List<IntObserver> observers = new List<IntObserver>();

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
        foreach (IntObserver o in observers) {

            o.OnVarChanged();
        }
    }

    public void AddObserver(IntObserver observer) {

        observers.Add(observer);
    }

    public void RemoveObserver(IntObserver observer) {
        observers.Remove(observer);

    }
}

 
