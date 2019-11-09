using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Spell{NONE,BOLT,SHIELD,RANGE_ATTACK};
public class SpellSlot : MonoBehaviour
{
    private bool isSpellEnabled = false;

    public Spell spell;
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

    public Spell getSpell() {
        return this.spell;
    }
}
