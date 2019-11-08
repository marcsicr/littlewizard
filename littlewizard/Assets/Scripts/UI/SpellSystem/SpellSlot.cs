using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSlot : MonoBehaviour
{
    private bool isSpellEnabled = false;

    private GameObject selectedImg;
   
    void Start()
    {
        selectedImg = transform.Find("selImg").gameObject;
     
    }

    public void setSelected(bool selected) {

        selectedImg.SetActive(selected);
    }

    /**
     * @Return true if player knows the spell
     */
    public bool spellEnabled() {

        return this.isSpellEnabled;
    }
}
