using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpellsHolder : MonoBehaviour{


    private Mouse cursor;

    public Transform slot1;
    public Transform slot2;
    public Transform slot3;

    
    private SpellSlot spell1;
    private SpellSlot spell2;
    private SpellSlot spell3;

    private SpellSlot active;
    void Start(){

        cursor = transform.parent.Find("Cursor").GetComponent<Mouse>();
        if (cursor == null)
            Debug.Log("CastManager: Cursor not found");

        active = null;
        spell1 = slot1.GetComponent<SpellSlot>();
        spell2 = slot2.GetComponent<SpellSlot>();
        spell3 = slot3.GetComponent<SpellSlot>();

    }

    public void onSpellSelectedChange() {

        Spell selected = LevelManager.Instance.selectedSpell;

        if (selected == Spell.BOLT) {

            spell1.setSelected(true);
            active = spell1;

            spell2.setSelected(false);
            spell3.setSelected(false);
            Debug.Log("Spell on slot 1 selected");
            return;
        }

        if (selected == Spell.SHIELD) {

            spell2.setSelected(true);
            active = spell2;
            spell1.setSelected(false);
            spell3.setSelected(false);
            Debug.Log("Spell on slot 2 selected");
            return;
        }

        if (selected == Spell.RANGE_ATTACK) {

            spell3.setSelected(true);
            active = spell3;
            spell1.setSelected(false);
            spell2.setSelected(false);
            Debug.Log("Spell on slot 3 selected");
            return;
        }
    }

    public void OnSpellCasted() {
        active.setSelected(false);
        active.showSpellTimeOut();
    }

}
