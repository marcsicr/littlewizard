using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Activable : MonoBehaviour
{
    // Start is called before the first frame update
    protected Mechanism mechanism;
    protected virtual void Start()
    {
       mechanism =  transform.parent.parent.gameObject.GetComponent<Mechanism>();
       if(mechanism == null) {
            Debug.Log("Activable: Parent mechanism not found");
            return;
        }
       mechanism.registerActivable(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Activate();

    public abstract void Desactivate();

}
