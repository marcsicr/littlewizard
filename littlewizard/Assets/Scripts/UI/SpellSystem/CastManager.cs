using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CastManager : MonoBehaviour
{
    public Transform slot1;
    public Transform slot2;
    public Transform slot3;

    
    private SpellSlot spell1;
    private SpellSlot spell2;
    private SpellSlot spell3;

    void Start(){

        spell1 = slot1.GetComponent<SpellSlot>();
        spell2 = slot2.GetComponent<SpellSlot>();
        spell3 = slot3.GetComponent<SpellSlot>();

    }

    // Update is called once per frame
    void Update(){

        handleInput();
    }

   
    void handleInput() {

        if (Input.GetMouseButtonDown(1)) {

            spell1.setSelected(false);
            spell2.setSelected(false);
            spell3.setSelected(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)){

            spell1.setSelected(true);
            spell2.setSelected(false);
            spell3.setSelected(false);
            Debug.Log("Spell on slot 1 selected");
            
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {

              spell2.setSelected(true);
              spell1.setSelected(false);
              spell3.setSelected(false);
            Debug.Log("Spell on slot 2 selected");
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) {

            spell3.setSelected(true);
            spell1.setSelected(false);
            spell2.setSelected(false);
            Debug.Log("Spell on slot 3 selected");
            return;
        }
    }
}
