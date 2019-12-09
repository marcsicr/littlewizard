using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellIndex : MonoBehaviour
{
    public Sprite selected;
    public Sprite unSelected;

    public Spell spell;

    SpellsBook book;

    private void Awake() {

        book = transform.parent.GetComponent<SpellsBook>();
    }

    public void onClick() {

        if(spell != Spell.NONE)
            book.showSpell(spell);

        Debug.Log("OnClick");
    }

    public void setSelected(bool isSelected) {

        Image img = GetComponent<Image>();

        if (isSelected) {
            img.sprite = selected;
        }else {
            img.sprite = unSelected;

        }
    }
}
