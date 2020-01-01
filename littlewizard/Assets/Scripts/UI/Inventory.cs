using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    GameObject keyIcon;

    public void Awake() {
        keyIcon = transform.Find("KeyIcon").gameObject;
    }
    public void onKeyCaught() {

        keyIcon.SetActive(true);

   }

    public void onKeyUsed() {

        keyIcon.SetActive(false);

    }
}
