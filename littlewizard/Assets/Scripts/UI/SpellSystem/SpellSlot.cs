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

       if (LevelManager.Instance.getSpellLevel(spell) == 0)
            GetComponent<Image>().color = new Color(0, 0, 0, 0);
    }

    public void OnSpellLvlUp(Spell spellLeveledUp) {

        if(this.spell == spellLeveledUp) {
            GetComponent<Image>().color = new Color(1,1, 1, 1);
        }
    }

    public void setSelected(bool selected) {

        spellState.setSelected(selected);
    }


    public void showSpellTimeOut() {
        spellState.setReloading(spell);
    }

}
