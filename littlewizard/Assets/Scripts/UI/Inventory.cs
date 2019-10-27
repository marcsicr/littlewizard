using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    GameObject keyIcon;
    bool hasKey;
    public void Awake() {
        keyIcon = transform.Find("KeyIcon").gameObject;
    }
    public void addKey() {

        if(keyIcon != null) {
            hasKey = true;
            keyIcon.SetActive(true);
        }
   }

    /*Return true if the user uses the key*/
    public bool useKey() {

        if (hasKey) {
           
            keyIcon.SetActive(false);
            hasKey = false;
            return true;
        }
        return false;
        
    }
}
