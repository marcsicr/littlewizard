using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SpellSlot : MonoBehaviour {

    public Spell spell;
    
    private SpellState spellState;
    private TextMeshProUGUI spellNumber;
    void Start() {
        spellState = transform.Find("SpellState").gameObject.GetComponent<SpellState>();
        spellNumber = transform.Find("SpellNumber").gameObject.GetComponent<TextMeshProUGUI>();
        
        //spellState.setReloadSpeed(reloadSpeed);

        if (SpellsManager.Instance.getSpellLevel(spell) == 0) {
            GetComponent<Image>().color = new Color(0, 0, 0, 0);
            spellNumber.gameObject.SetActive(false);
        }
          
    }

    public void OnSpellLvlUp(Spell spellLeveledUp) {

        if(this.spell == spellLeveledUp) {
            GetComponent<Image>().color = new Color(1,1, 1, 1);
            spellNumber.gameObject.SetActive(true);
        }
    }

    public void setSelected(bool selected) {

        spellState.setSelected(selected);
    }


    public void showSpellTimeOut() {
        spellState.setReloading(spell);
    }

}
