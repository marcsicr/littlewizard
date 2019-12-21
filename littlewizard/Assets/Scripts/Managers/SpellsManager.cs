using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Spell { NONE, BOLT, SHIELD, RANGE_ATTACK };
public class SpellsManager : MonoBehaviour {


    public static SpellsManager Instance { get; private set; }

    public int boltLevel { get; private set; }
    public int shieldLevel { get; private set; }
    public int rayLevel { get; private set; }

   
    public Spell selectedSpell = Spell.NONE;
    
    public SpellSignal spellSelected;
    public float boltTimeOut{ get; private set; }
    public float shieldTimeOut{ get; private set; }
    public float rayAtkTimeOut { get; private set; }


    private void Awake() {

        if (Instance == null) {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        } else {

            Destroy(gameObject);
        }

    }

    void Start() { 
        boltLevel = GameManager.Instance.baseBoltLvl;
        shieldLevel = GameManager.Instance.baseShieldLvl;
        rayLevel = GameManager.Instance.baseRayLvl;
    }


  
    public void resetInstance() {
        boltLevel = GameManager.Instance.baseBoltLvl;
        shieldLevel = GameManager.Instance.baseShieldLvl;
        rayLevel = GameManager.Instance.baseRayLvl;
    }

    public void destory() {
        Instance = null;
    }

    private void handleSpellsInput() {

        if (Input.GetKeyDown(KeyCode.Alpha1) && boltTimeOut == 0 &&boltLevel >0 ) {
            selectedSpell = Spell.BOLT;
            spellSelected.Raise(selectedSpell);
           
        } else if (Input.GetKeyDown(KeyCode.Alpha2) && shieldTimeOut == 0  && shieldLevel >0) {
            selectedSpell = Spell.SHIELD;
            spellSelected.Raise(selectedSpell);
           
        } else if (Input.GetKeyDown(KeyCode.Alpha3) && rayAtkTimeOut == 0 && rayLevel > 0) {

            selectedSpell = Spell.RANGE_ATTACK;
            spellSelected.Raise(selectedSpell);
          
        }

    }

    private void Update() {
        handleSpellsInput();
        spellsTimeOutUpdate();
    }

    public void OnSpellCasted(Spell spell) {

        float spellTimeOut = computeSpellTimeOut(spell);
        setSpellTimeOut(spell,spellTimeOut);
        selectedSpell = Spell.NONE;
    }

    private void setSpellTimeOut(Spell spell,float timeOut) {

        switch (spell) {

            case Spell.BOLT: {
                    boltTimeOut = timeOut;
                    break;
                }

            case Spell.SHIELD: {
                    shieldTimeOut = timeOut;
                    break;
                }

            case Spell.RANGE_ATTACK: {
                    rayAtkTimeOut = timeOut;
                    break;
                }
        }
    }

    public float computeSpellTimeOut(Spell spell) {

        switch (spell) {

            case Spell.BOLT: {
                    return 6 - boltLevel;
            }

            case Spell.SHIELD: {
                    return 12-shieldLevel;
            }

            case Spell.RANGE_ATTACK: {
                    return 10-rayLevel;
            }
        }

        return 0;
    }

    public float computeSpellDuration(Spell spell) {

        switch (spell) {

          
            case Spell.SHIELD: {
                    return 3 + shieldLevel;
                }

            case Spell.RANGE_ATTACK: {
                    return 2 + rayLevel *0.5f;
                }
        }

        return 0;
    }

    public void spellLvlUp(Spell spell) {

        if(spell == Spell.BOLT) {
            boltLevel += 1;
            return;
        }

        if (spell == Spell.SHIELD) {
            shieldLevel += 1;
            return;
        }

        if (spell == Spell.RANGE_ATTACK) {
            rayLevel += 1;
            return;
        }
   }


    public void spellsTimeOutUpdate() {

        if(boltTimeOut > 0) {
            boltTimeOut = Mathf.Clamp(boltTimeOut - Time.deltaTime, 0, boltTimeOut);
        }
        if(shieldTimeOut > 0) {
            shieldTimeOut = Mathf.Clamp(shieldTimeOut - Time.deltaTime, 0, shieldTimeOut);
        }

        if(rayAtkTimeOut > 0) {
            rayAtkTimeOut = Mathf.Clamp(rayAtkTimeOut - Time.deltaTime, 0, rayAtkTimeOut);
        }
    }

    public int getSpellLevel(Spell spell) {

        switch (spell) {

            case Spell.BOLT: {
                    return boltLevel;
                }

            case Spell.SHIELD: {
                    return shieldLevel;
                }

            case Spell.RANGE_ATTACK: {
                    return rayLevel;
                }
        }

        return 0;
    }

    public int computeSPConsumed(Spell spell) {

        switch (spell) {

            case Spell.BOLT: {
                    return 30 - 5 * boltLevel;
            }

            case Spell.SHIELD: {
                    return 35 -5* shieldLevel;
                }

            case Spell.RANGE_ATTACK: {
                    return 35 - 5 * boltLevel;
                }
        }

        return 0;

    }

    public int computeSpellDamage(Spell spell) {

        switch (spell) {

            case Spell.BOLT:
                return 5 * boltLevel;

            case Spell.RANGE_ATTACK:
                return  1* rayLevel;

            default:
                return 0;
            
        }
    }

    public float getSpellTimeout(Spell spell) {

        switch (spell) {

            case Spell.BOLT: {
                    return boltTimeOut;   
            }

            case Spell.SHIELD: {
                    return shieldTimeOut;
            }
            
            case Spell.RANGE_ATTACK: {
                    return rayAtkTimeOut;    
            }
        }

        return 0;
    }

}