using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffKick : MonoBehaviour
{
    public int KickPower;
    public void Update() {

       
    }
    public void OnTriggerEnter2D(Collider2D other) {


        if (other.gameObject.tag == Enemy.ENEMY_TAG) {
            Enemy enemy = (Enemy) other.gameObject.GetComponent(typeof(Enemy));
            enemy.OnGetKicked(KickPower);
            
        }else if (other.CompareTag("Pot")) {

            Pot pot = other.GetComponent<Pot>();
            pot.destroy();
        }

    }
}
