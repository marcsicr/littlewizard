using System;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SpellEvent : UnityEvent<Spell> {}
public class SpellSignalListener : MonoBehaviour {

    public SpellSignal signal;
    public SpellEvent response;


    private void OnEnable() {
        signal.AddListener(this);
    }

    private void OnDisable() {
        signal.RemoveListener(this);
    }

    public void OnSignalReceived(Spell spell) {
        response.Invoke(spell);
    }
}

