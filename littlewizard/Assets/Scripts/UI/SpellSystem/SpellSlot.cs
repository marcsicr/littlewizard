using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpellSlot : MonoBehaviour {
    private bool isSpellEnabled = false;

    private bool isSelected = false;
    public Spell spell;

    private SpellState spellState;

    void Start() {
        spellState = transform.Find("SpellState").gameObject.GetComponent<SpellState>();
        //spellState.setReloadSpeed(reloadSpeed);

    }

    public void setSelected(bool selected) {

        spellState.setSelected(selected);
    }

    /**
     * @Return true if player knows the spell
     */
    public bool spellEnabled() {

        return this.isSpellEnabled;
    }

    public void showSpellTimeOut() {
        spellState.setReloading(spell);
    }

}
