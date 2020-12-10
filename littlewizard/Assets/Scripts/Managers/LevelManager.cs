using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Text.RegularExpressions;
using System;

public class LevelManager : MonoBehaviour {


    public static LevelManager Instance { get; private set; }

    private PathsManager pathManager;

    public Zone[] zones;

    //public BoundsManager initialBounds;

    public Signal gemCaughtSignal;
    
    public Signal keyCaught;
    public Signal keyUsed;

    private Tilemap heightsMap;

    int gemsInLevel;
    int gemsCaught;

    bool hasKey;

    

    private void Awake() {

        if (Instance == null) {
            Instance = this;

            gemsInLevel = 0;
            gemsCaught = 0;
            heightsMap = GameObject.FindGameObjectWithTag("HeightsMap").GetComponent<Tilemap>();
            pathManager = GetComponent<PathsManager>();

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

    

    public Zone startZone() {

        Zone playerZone = getZone(Player.GetPlayer().transform.position);
        SoundManager.Instance.changeSong(playerZone.zoneSong);
        return playerZone;
    }

    public int gemsCaughtCount() {
        return gemsCaught;
    }

    public int gemsOnLevel() {
        return gemsInLevel;
    }


    public bool useKey() {

        if (hasKey) {
            hasKey = false;
            keyUsed.Raise();
            return true;
        }

        return false;
    }
    public void addKey() {
        hasKey = true;
        keyCaught.Raise();
    }

    

    public void resetInstance() {

        gemsInLevel = 0;
        gemsCaught = 0;
        hasKey = false;
    
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


    public Zone getZone(Vector3 worldPosition) {

        foreach(Zone z in zones) {

            if (z.isInsideZone(worldPosition))
                return z;
        }

        return null;
    }

    public void ComputePath(Vector3 start, Vector3 target, Action<Vector3[]> callback) {
        pathManager.ComputePath(start, target, callback);
    }
}