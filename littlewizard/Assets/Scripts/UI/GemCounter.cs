using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemCounter : MonoBehaviour {
    private TextMeshProUGUI text;
    
  
    // Start is called before the first frame update
    void Start() {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        UpdateCounter();
    }

    // Update is called once per frame
    void Update() {

    }

    public void UpdateCounter() {

        int gemsCaught = LevelManager.Instance.gemsCaughtCount();
        int gemsOnLevel = LevelManager.Instance.gemsOnLevel();

        text.SetText(gemsCaught.ToString() + "/" + gemsOnLevel.ToString());

    }

}
