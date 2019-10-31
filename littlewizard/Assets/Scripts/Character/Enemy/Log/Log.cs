using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    private float wakeDistance = 2f;
    //private float minDistance = 2f;
    public GameObject root;
    private RootAttack aRoot;
    
    
    public override void  Start(){
        base.Start();

        minDistance = 2f;
        root = Instantiate(root, transform.position, Quaternion.identity);
        aRoot = root.GetComponent<RootAttack>();
        aRoot.setAttackPower(this.attackPower);
    }

    public void Update(){
        
    }

    public override void OnGetKicked(int attack) {

        this.HP -= attack;
        if (this.HP <= 0) {

            Destroy(gameObject);
        }

        bar.updateBar(HP);

    }
    
    public bool shouldWakeUp() {
        return distanceFromPlayer() <= wakeDistance ? true : false;
    }

    public float getMinDistance() {
        return this.minDistance;
    }

    
    protected override void attackAction() {
        aRoot.attack(getTarget());
    }
}
