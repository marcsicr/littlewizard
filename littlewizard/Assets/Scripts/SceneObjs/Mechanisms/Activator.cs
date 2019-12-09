using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Activator : MonoBehaviour
{
    protected Mechanism mechanism;
    protected bool state;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        state = false;
        mechanism = transform.parent.parent.gameObject.GetComponent<Mechanism>();
        if (mechanism == null) {
            Debug.Log("Activable: Parent mechanism not found");
            return;
        }
        mechanism.registerActivator(this);
    }

    public abstract void Reset();


    public bool getState() {

        return state;
    }
}
