using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpellSignal : ScriptableObject
{
    private List<SpellSignalListener> listeners = new List<SpellSignalListener>();

    public void Raise(Spell spell) {

        foreach (SpellSignalListener l in listeners) {

            l.OnSignalReceived(spell);
        }
    }

    public void AddListener(SpellSignalListener listener) {

        listeners.Add(listener);
    }

    public void RemoveListener(SpellSignalListener listener) {
        listeners.Remove(listener);
    }
}
