using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Text.RegularExpressions;

public class LevelManager : MonoBehaviour {


    public static LevelManager Instance { get; private set; }

    public int boltLevel { get; private set; }
    public int shieldLevel { get; private set; }
    public int rayLevel { get; private set; }

    public Signal gemCaughtSignal;
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

}