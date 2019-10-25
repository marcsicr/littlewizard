using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Signal : ScriptableObject
{
    private List<SignalListener> listeners = new List<SignalListener>();

    public void Raise() {

        foreach (SignalListener l in listeners) {

            l.OnSignalReceived();
        }
    }

    public void AddListener(SignalListener listener) {

        listeners.Add(listener);
    }

    public void RemoveListener(SignalListener listener) {
        listeners.Remove(listener);
    }
}
