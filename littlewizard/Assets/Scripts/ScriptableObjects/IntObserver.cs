using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IntEvent : UnityEvent<ObservableInt> {
}
public class IntObserver : MonoBehaviour
{
    public ObservableInt var;
    public IntEvent response;

    public UnityEvent<int> e;

    private void OnEnable() {
        var.AddObserver(this);
    }

    private void OnDisable() {
        var.RemoveObserver(this);
    }

    public void OnVarChanged() {
        response.Invoke(var);
    }
}
