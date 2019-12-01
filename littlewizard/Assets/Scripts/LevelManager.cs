using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance { get; private set; }

    public Signal gemCaughtSignal;

    Dictionary<int, GameObject> dictionary;
    List<GameObject> gemsCaught;

    private void Awake() {

        if (Instance == null) {
            Instance = this;

            dictionary = new Dictionary<int, GameObject>();
            gemsCaught = new List<GameObject>();

            DontDestroyOnLoad(gameObject);
        } else {

            Destroy(gameObject);
        }
    }

    public void registerGem(GameObject gem) {
        dictionary.Add(gem.GetInstanceID(), gem);
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
    }

}
