using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    private float wakeDistance = 2f;
    private float minDistance = 2f;
    public float attackTimeout = 3f;
    private float nextAttack;
    public GameObject root;
    private RootAttack aRoot;
    
    
    public override void  Start(){
        base.Start();
        //root = Instantiate()
        nextAttack = Time.time + attackTimeout;
        root = Instantiate(root, transform.position, Quaternion.identity);
        aRoot = root.GetComponent<RootAttack>();
        aRoot.setAttackPower(this.attackPower);
    }

    public void Update(){
        
    }

    public override void OnGetKicked(int attack) {

        
        Destroy(this.gameObject);
        Destroy(this.root);
    }
    
    public bool shouldWakeUp() {
        return distanceFromPlayer() <= wakeDistance ? true : false;
    }

    public float getMinDistance() {
        return this.minDistance;
    }

    public void rootAttack() {

        if(Time.time > nextAttack) {

            //Debug.Log("Log attacks player");
            aRoot.attack(getTarget());
            nextAttack = Time.time + attackTimeout;// + Random.Range(0,10f);
        } 
    }
}
