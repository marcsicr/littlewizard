using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffKick : MonoBehaviour
{
    public void Update() {

       
    }
    public void OnTriggerEnter2D(Collider2D other) {


        if (other.gameObject.tag == Enemy.ENEMY_TAG) {

            Log enemy =(Log) other.gameObject.GetComponent(typeof(Enemy));
            enemy.OnGetKicked(1);
            
        }
    }
}
