using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCounter : Activator {

   
    private int enemiesCurrent;
    private bool notified = false;
    private Counter counter;
    void Start() {
        base.Start();
        Debug.Log("Number of enemies:" + transform.GetComponentsInChildren<AbstractEnemy>().Length);

        enemiesCurrent = transform.GetComponentsInChildren<AbstractEnemy>().Length;

        counter = transform.GetComponentInChildren<Counter>();
        counter.setText(enemiesCurrent.ToString());
    }


    void Update() {

        int current = transform.GetComponentsInChildren<AbstractEnemy>().Length;
        if( current != enemiesCurrent) {
            enemiesCurrent = current;
            counter.setText(enemiesCurrent.ToString());
        }

        if(enemiesCurrent == 0 && !notified) {
            state = true;
            mechanism.notifyStatusChange(this);
        }
    }

    public override void Reset() {
    }
}
