using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mechanism : MonoBehaviour
{
    List<Activable> activables;
    //List<Activator> activators; // Objects that must be activated to activite the mechanism
    private Dictionary<int, bool> activators;

    
    void Awake()
    {
        activables = new List<Activable>(); // Objects to be activated by mechanism
        activators = new Dictionary<int, bool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void registerActivable(Activable activable) {
        activables.Add(activable);
    }

    public void registerActivator(Activator activator) {

        activators.Add(activator.GetInstanceID(), activator.getState());
    }

    public void notifyStatusChange(Activator activator) {

        activators[activator.GetInstanceID()] = activator.getState();

        //Calculate if mechanism is activated

        computeMechanismeStatus();
    }

    
    private void computeMechanismeStatus() {

        bool activate = true;

        foreach( KeyValuePair<int,bool> id in activators) {

            activate &= id.Value;
        }

        notifyMechanismStatus(activate);
    }

    private void notifyMechanismStatus(bool isActivated) {

        if (isActivated) {
            foreach (Activable activable in activables) {
                activable.Activate();
            }

        } else {

            foreach (Activable activable in activables) {
                activable.Desactivate();
            }
        }
        
    }
  
}
