using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Text.RegularExpressions;

public class LevelManager : MonoBehaviour {


    public static LevelManager Instance { get; private set; }


    public BoundsManager initialBounds;

    public Signal gemCaughtSignal;

    private Tilemap heightsMap;

    int gemsInLevel;
    int gemsCaught;

    

    private void Awake() {

        if (Instance == null) {
            Instance = this;

            gemsInLevel = 0;
            gemsCaught = 0;
            heightsMap = GameObject.FindGameObjectWithTag("HeightsMap").GetComponent<Tilemap>();

            DontDestroyOnLoad(gameObject);
        } else {

            Destroy(gameObject);
        }

    }


    public void registerGem(GameObject gem) {

        gemsInLevel++;
        gemCaughtSignal.Raise();

    }

    public void addGemCaught(GameObject gem) {
        gemsCaught++;
        gemCaughtSignal.Raise();

    }

    public RectBoundaries startBoundaries() {

        SoundManager.Instance.changeSong(initialBounds.zoneSong);
        return initialBounds.getBoundaries();
    }

    public int gemsCaughtCount() {
        return gemsCaught;
    }

    public int gemsOnLevel() {
        return gemsInLevel;
    }


    public void resetInstance() {

        gemsInLevel = 0;
        gemsCaught = 0;
    
    }

    public void destory() {
        Instance = null;
    }


    public int getTileLevel(Vector3 worldPosition) {

        Tile tile = heightsMap.GetTile<Tile>(heightsMap.WorldToCell(worldPosition));
        if (tile == null) {
            return 999;
        }

        Regex regex = new Regex(@"\d+");
        Match m = regex.Match(tile.sprite.name);
        return int.Parse(m.Value);
    }

}