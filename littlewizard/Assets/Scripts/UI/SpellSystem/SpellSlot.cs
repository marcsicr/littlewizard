using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Spell{NONE,BOLT,SHIELD,RANGE_ATTACK};
public class SpellSlot : MonoBehaviour
{
    private bool isSpellEnabled = false;

    public Spell spell;
    public float reloadSpeed;
    private SpellState spellState;
   
    void Start()
    {
        spellState = transform.Find("SpellState").gameObject.GetComponent<SpellState>();
        spellState.setReloadSpeed(reloadSpeed);
     
    }

    public bool setSelected(bool selected) {
       return spellState.setSelected(selected);
    }

    public void spellCasted() {

        spellState.setReloading();
    }

    /**
     * @Return true if player knows the spell
     */
    public bool spellEnabled() {

        return this.isSpellEnabled;
    }

    public Spell getSpellToCast() {
        spellState.setCasted();
        return this.spell;
    }

    public Spell getSpell() {
        return this.spell;
    }
}
