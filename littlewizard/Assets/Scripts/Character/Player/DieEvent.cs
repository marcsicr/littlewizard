using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieEvent : MonoBehaviour
{
  
    public void die() {

        Player p = gameObject.GetComponentInParent<Player>();
        if (p == null)
            Debug.Log("DieEvent: Character not found");
        

        p.die();

        
    }

    public void explosionEnd() {
        gameObject.SetActive(false);
    }
}
