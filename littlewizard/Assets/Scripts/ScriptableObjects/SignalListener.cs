using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public Signal signal;
    public UnityEvent response;

    private void OnEnable() {
        signal.AddListener(this);
    }

    private void OnDisable() {
        signal.RemoveListener(this);
    }

    public void OnSignalReceived() {
        response.Invoke();
    }
}
