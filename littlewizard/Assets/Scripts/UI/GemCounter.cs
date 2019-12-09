using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemCounter : MonoBehaviour {
    private TextMeshProUGUI text;

    private void Awake() {
        text = gameObject.GetComponent<TextMeshProUGUI>();
    }
    // Start is called before the first frame update
    void Start() {
       // text = gameObject.GetComponent<TextMeshProUGUI>();
        //UpdateCounter();
    }

    public void UpdateCounter() {

        int gemsCaught = LevelManager.Instance.gemsCaughtCount();
        int gemsOnLevel = LevelManager.Instance.gemsOnLevel();

        text.SetText(gemsCaught.ToString() + "/" + gemsOnLevel.ToString());

    }

}
