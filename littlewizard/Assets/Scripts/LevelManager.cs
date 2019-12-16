using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Text.RegularExpressions;

public enum Spell { NONE, BOLT, SHIELD, RANGE_ATTACK };
public class LevelManager : MonoBehaviour {


    public static LevelManager Instance { get; private set; }

    public int boltLevel { get; private set; }
    public int shieldLevel { get; private set; }
    public int rayLevel { get; private set; }

    public Signal gemCaughtSignal;
    public Spell selectedSpell = Spell.NONE;
    public Signal spellSelected;

    public float boltTimeOut{ get; private set; }
    public float shieldTimeOut{ get; private set; }
    public float rayAtkTimeOut { get; private set; }


    private Tilemap heightsMap;

    Dictionary<int, GameObject> dictionary;
    List<GameObject> gemsCaught;

    private void Awake() {

        if (Instance == null) {
            Instance = this;

            dictionary = new Dictionary<int, GameObject>();
            gemsCaught = new List<GameObject>();
            heightsMap = GameObject.FindGameObjectWithTag("HeightsMap").GetComponent<Tilemap>();

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


    public void registerGem(GameObject gem) {
        dictionary.Add(gem.GetInstanceID(), gem);
        gemCaughtSignal.Raise();
    }

    public void addGemCaught(GameObject gem) {
        gemsCaught.Add(gem);
        gemCaughtSignal.Raise();

    }

    public int gemsCaughtCount() {
        return gemsCaught.Count;
    }

    public int gemsOnLevel() {
        return dictionary.Count;
    }


    public void resetInstance() {

        dictionary = new Dictionary<int, GameObject>();
        gemsCaught = new List<GameObject>();
        boltLevel = GameManager.Instance.baseBoltLvl;
        shieldLevel = GameManager.Instance.baseShieldLvl;
        rayLevel = GameManager.Instance.baseRayLvl;
    }

    public void destory() {
        Instance = null;
    }


    public int getTileLevel(Vector3 worldPosition) {

        Tile tile = heightsMap.GetTile<Tile>(heightsMap.WorldToCell(worldPosition));
        if(tile == null) {
            return 999;
        }

        Regex regex = new Regex(@"\d+");
        Match m = regex.Match(tile.sprite.name);
        return int.Parse(m.Value);
    }

    private void handleSpellsInput() {

        if (Input.GetKeyDown(KeyCode.Alpha1) && boltTimeOut == 0 /*&& boltLevel >0*/) {
            selectedSpell = Spell.BOLT;
            spellSelected.Raise();
        } else if (Input.GetKeyDown(KeyCode.Alpha2) && shieldTimeOut == 0 /*&& shieldLevel >0*/) {
            selectedSpell = Spell.SHIELD;
            spellSelected.Raise();
        } else if (Input.GetKeyDown(KeyCode.Alpha3) && rayAtkTimeOut == 0 /*&& rayLevel > 0*/) {
            selectedSpell = Spell.RANGE_ATTACK;
            spellSelected.Raise();
        }

    }

    private void Update() {
        handleSpellsInput();

        spellsTimeOutUpdate();
    }

    public void OnSpellCasted() {

        float spellTimeOut = computeSpellTimeOut(selectedSpell);
        setSpellTimeOut(selectedSpell,spellTimeOut);
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
                    return 2;
            }

            case Spell.SHIELD: {
                    return 6;
            }

            case Spell.RANGE_ATTACK: {
                    return 4;
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